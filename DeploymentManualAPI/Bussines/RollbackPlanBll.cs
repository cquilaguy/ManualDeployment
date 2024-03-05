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
    public class RollbackPlanBll:IRollbackPlanBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISRollbackPlan _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISPlan _PlanRepository;
        private readonly ILogger<RollbackPlanBll> _logger;




        public RollbackPlanBll(ISRollbackPlan repository, MessageResponse msgProvider, ISPlan planRepository,
           ILogger<RollbackPlanBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _PlanRepository = planRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los rollback de plan teniendo en cuenta un PlanID
        /// </summary>
        /// <param name="PlanID"></param>
        /// <returns>Retorna una colección de tipo RollbackPlan con todos los rollback de plan</returns>
        public async Task<ServiceResponse> GetRollbackPlan(int PlanID)
        {
            try
            {
                var data = await _Repository.GetRollbackPlan(PlanID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros de rollback de plan con el PlanID proporcionado, en la tabla RollbackPlan.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los rollback de plan con PlanID: " + PlanID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un rollback de plan
        /// </summary>
        /// <param name="rollbackPlan"></param>
        /// <returns>Retorna una JSON con el rollback de plan registrado</returns> 
        public async Task<ServiceResponse> PostRollbackPlan(RollbackPlanDTO rollbackPlan)
        {
            try
            {
                RollbackPlan result = await _Repository.PostRollbackPlan(rollbackPlan);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de rollback de plan - Data: {@RollbackPlan}", rollbackPlan);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de plan
        /// </summary>
        /// <param name="rollbackPlan"></param>
        /// <returns>Retorna un JSON con el rollback de plan actualizado</returns>
        public async Task<ServiceResponse> PutRollbackPlan(RollbackPlanDTO rollbackPlan)
        {
            try
            {
                if (await _Repository.RollbackPlanExists(rollbackPlan.RollbackPlanID))
                {
                    RollbackPlan data = await _Repository.PutRollbackPlan(rollbackPlan);
                    if (data == null)
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron planes con el ChangeID o RollbackPlanID o secuencia proporcionado en la tabla RollbackPlan.");
                    }
                    else
                    {
                        return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                    }
                    
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de plan o rollback de plan no existe en la tabla RollbackPlan.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@RollbackPlan}", rollbackPlan);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="rollbackplan"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(RollbackPlanDTO rollbackplan)
        {
            try
            {
                if (!await _PlanRepository.PlanExists(rollbackplan.PlanID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de rollback de plan - Data: {@RollbackPlan}", rollbackplan);
                return false;
            }
        }
    }
}
