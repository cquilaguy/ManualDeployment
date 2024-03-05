using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class RequestTypeBll:IRequestTypeBll
    {
        private readonly ISRequestType _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<RequestTypeBll> _logger;

        public RequestTypeBll(ISRequestType repository, MessageResponse msgProvider, ILogger<RequestTypeBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo para obtener los tipos de solicitud 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse> GetRequestTypes()
        {
            try
            {
                IEnumerable<RequestType> data = await _Repository.GetRequestTypes();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los tipos de solicitud", ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
