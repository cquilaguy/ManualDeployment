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
    public class BlueprintBll:IBlueprintBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISBlueprint _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISApplicative _ApplicativeRepository;
        private readonly ILogger<BlueprintBll> _logger;




        public BlueprintBll(ISBlueprint repository, MessageResponse msgProvider, ISApplicative applicativeRepository,
           ILogger<BlueprintBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _ApplicativeRepository = applicativeRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los blueprints teniendo en cuenta un ApplicativeID
        /// </summary>
        /// <param name="ApplicativeID"></param>
        /// <returns>Retorna una colección de tipo Blueprint con todos los blueprint</returns>
        public async Task<ServiceResponse> GetBlueprint(int ApplicativeID)
        {
            try
            {
                var data = await _Repository.GetBlueprint(ApplicativeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros de blueprints con el ApplicativeID proporcionado, en la tabla Blueprint.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los blueprints con ApplicativeID: " + ApplicativeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un blueprint
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna una JSON con el blueprint registrado</returns> 
        public async Task<ServiceResponse> PostBlueprint(BlueprintDTO blueprint)
        {
            try
            {
                Blueprint result = await _Repository.PostBlueprint(blueprint);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de rollback de plan - Data: {@Blueprint}", blueprint);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de plan
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna un JSON con el blueprint actualizado</returns>
        public async Task<ServiceResponse> PutBlueprint(BlueprintDTO blueprint)
        {
            try
            {
                if (await _Repository.BlueprintExists(blueprint.BlueprintID))
                {
                    Blueprint data = await _Repository.PutBlueprint(blueprint);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Blueprint o aplicativo no existe en la tabla Blueprint.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de blueprint - Data: {@Blueprint}", blueprint);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(BlueprintDTO blueprint)
        {
            try
            {
                if (!await _ApplicativeRepository.ApplicativeExists(blueprint.ApplicativeID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de blueprint - Data: {@Blueprint}", blueprint);
                return false;
            }
        }
    }
}
