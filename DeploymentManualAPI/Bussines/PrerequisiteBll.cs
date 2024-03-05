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
    public class PrerequisiteBll:IPrerequisiteBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISPrerequisite _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISUser _UserRepository;
        private readonly ISResponsibleArea _ResponsibleAreaRepository;
        private readonly ILogger<PrerequisiteBll> _logger;




        public PrerequisiteBll(ISPrerequisite repository, MessageResponse msgProvider, ISChange changeRepository,
            ISUser userRepository, ISResponsibleArea responsibleAreaRepository, ILogger<PrerequisiteBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _ChangeRepository = changeRepository;
            _UserRepository = userRepository;
            _ResponsibleAreaRepository = responsibleAreaRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los prerrequisitos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna un JSON con la información de los prerrequisitos en especifico</returns>
        public async Task<ServiceResponse> GetPrerequisite(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetPrerequisite(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron Prerequisitos con el ChangeID proporcionado en la tabla Prerequisite.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los prerrequisitos con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un prerrequisito
        /// </summary>
        /// <param name="Prerequisite"></param>
        /// <returns>Retorna una JSON con el prerrequisito registrado</returns>
        public async Task<ServiceResponse> PostPrerequisite(PrerequisiteDTO prerequisite)
        {
            try
            {
                if (await _Repository.PrerequisiteExists(prerequisite.PrerequisiteID))
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Este PrerequisiteID ya se encuentra registrado");
                }

                else
                {
                    if (await _Repository.CheckSequence(prerequisite.ChangeID, prerequisite.Sequence))
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"Ya existe una secuencia: {prerequisite.Sequence} para ese PrerequisiteID y ChangeID");
                    }
                    else
                    {
                        Prerequisite result = await _Repository.PostPrerequisite(prerequisite);
                        return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de prerrequisito - Data: {@Prerequisite}", prerequisite);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un prerrequisito
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <returns>Retorna un JSON con el prerrequisito actualizado</returns>
        public async Task<ServiceResponse> PutPrerequisite(PrerequisiteDTO prerequisite)
        {
            try
            {
                if (await _Repository.PrerequisiteExists(prerequisite.PrerequisiteID))
                {
                    Prerequisite data = await _Repository.PutPrerequisite(prerequisite);
                    if (data == null)
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron prerequisitos con el ChangeID o prerequisito o secuencia proporcionado en la tabla Prerequisite.");
                    }
                    else 
                    {
                        return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                    }
                    
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Cambio o Prerequisito o secuencia no existe en la tabla Prerequisite.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de prerequisito - Data: {@Prerequisite}", prerequisite);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(PrerequisiteDTO prerequisite)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(prerequisite.ChangeID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(prerequisite.UserID))
                {
                    return false;
                }
                if (!await _ResponsibleAreaRepository.ResponsibleAreaExists(prerequisite.ResponsibleAreaID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@Prerequisite}", prerequisite);
                return false;
            }
        }
    }
}
