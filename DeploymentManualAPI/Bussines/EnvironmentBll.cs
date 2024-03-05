using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class EnvironmentBll: IEnvironmentBll
    {
        private readonly ISEnvironment _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<EnvironmentBll> _logger;
        public EnvironmentBll(ISEnvironment repository, MessageResponse msgProvider, ILogger<EnvironmentBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que retorna todos los ambientes
        /// </summary>
        /// <returns>Retorna una colección de tipo Environment con todos los ambientes registrados</returns>
        public async Task<ServiceResponse> GetEnvironments()
        {
            try
            {
                IEnumerable<Repository.Entities.Environment> data = await _Repository.GetEnvironments();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los ambientes", ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
