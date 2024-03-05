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
    public class RollbackPreBll:IRollbackPreBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISRollbackPre _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISPrerequisite _PrerequisiteRepository;
        private readonly ILogger<RollbackPreBll> _logger;




        public RollbackPreBll(ISRollbackPre repository, MessageResponse msgProvider, ISPrerequisite prerequisiteRepository,
           ILogger<RollbackPreBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _PrerequisiteRepository = prerequisiteRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los rollback de prerrequisito teniendo en cuenta un PrerequisiteID
        /// </summary>
        /// <param name="PrerequisiteID"></param>
        /// <returns>Retorna una colección de tipo RollbackPre con todos los rollback de prerrequisito</returns>
        public async Task<ServiceResponse> GetRollbackPre(int PrerequisiteID)
        {
            try
            {
                var data = await _Repository.GetRollbackPre(PrerequisiteID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros de rollback de prerequisitos con el PrerequisiteID proporcionado, en la tabla RollbackPre.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los rollback de prerrequisito con PrerequisiteID: " + PrerequisiteID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un rollback de prerrequisito
        /// </summary>
        /// <param name="rollbackPre"></param>
        /// <returns>Retorna una JSON con el rollback de prerrequisito registrado</returns> 
        public async Task<ServiceResponse> PostRollbackPre(RollbackPreDTO rollbackPre)
        {
            try
            {
                RollbackPre result = await _Repository.PostRollbackPre(rollbackPre);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de rollback de prerrequisito - Data: {@RollbackPre}", rollbackPre);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de prerrequisito
        /// </summary>
        /// <param name="rollbackPre"></param>
        /// <returns>Retorna un JSON con el rollback de prerrequisito actualizado</returns>
        public async Task<ServiceResponse> PutRollbackPre(RollbackPreDTO rollbackPre)
        {
            try
            {
                if (await _Repository.RollbackPreExists(rollbackPre.RollbackPreID))
                {
                    RollbackPre data = await _Repository.PutRollbackPre(rollbackPre);
                    if (data == null)
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron planes con el ChangeID o RollbackPreID o secuencia proporcionado en la tabla RollbackPre.");
                    }
                    else
                    {
                        return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                    }
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Prerequisito o rollback de prerequisito no existe en la tabla RollbackPre.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@RollbackPre}", rollbackPre);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="Rollbackpre"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(RollbackPreDTO Rollbackpre)
        {
            try
            {
                if (!await _PrerequisiteRepository.PrerequisiteExists(Rollbackpre.PrerequisiteID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de rollback de prerrequisito - Data: {@RollbackPre}", Rollbackpre);
                return false;
            }
        }
    }
}
