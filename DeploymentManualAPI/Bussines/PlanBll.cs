using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class PlanBll:IPlanBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISPlan _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISResponsibleArea _ResponsibleAreaRepository;
        private readonly ILogger<PlanBll> _logger;




        public PlanBll(ISPlan repository, MessageResponse msgProvider, ISChange changeRepository,
            ISResponsibleArea responsibleAreaRepository, ILogger<PlanBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _ChangeRepository = changeRepository;
            _ResponsibleAreaRepository = responsibleAreaRepository;
            _logger = logger;

        }

        /// <summary>
        /// Metodo que permite obtener los planes de implementación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo Plan con todos los planes de implementación/returns>
        public async Task<ServiceResponse> GetPlan(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetPlan(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron Planes con el ChangeID proporcionado en la tabla Plan.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos planes con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un plan de implementación
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna una JSON con el plan de implementación registrado</returns>
        public async Task<ServiceResponse> PostPlan(PlanDTO plan)
        {
            try
            {
                if (await _Repository.PlanExists(plan.PlanID))
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Este PlanID ya se encuentra registrado");
                }

                else
                {
                    if (await _Repository.CheckSequence(plan.ChangeID,plan.Sequence)){
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"Ya existe una secuencia: {plan.Sequence} para ese planID y ChangeID");
                    }
                    else {
                        Plan result = await _Repository.PostPlan(plan);
                        return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de plan de implementación - Data: {@Plan}", plan);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un plan de implementación
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna un JSON con el plan de implementación actualizado</returns>
        public async Task<ServiceResponse> PutPlan(PlanDTO plan)
        {
            try
            {
                if (await _Repository.PlanExists(plan.PlanID))
                {
                    Plan data = await _Repository.PutPlan(plan);
                    if (data == null)
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron planes con el ChangeID o planID o secuencia proporcionado en la tabla Plan.");
                    }
                    else {
                        return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                    }
                    
                    
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Cambio o Plan no existe en la tabla Plan.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de plan de implementación - Data: {@Plan}", plan);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(PlanDTO plan)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(plan.ChangeID))
                {
                    return false;
                }
                if (!await _ResponsibleAreaRepository.ResponsibleAreaExists(plan.ResponsibleAreaID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@Plan}", plan);
                return false;
            }
        }
    }
}
