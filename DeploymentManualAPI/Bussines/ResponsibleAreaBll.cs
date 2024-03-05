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
    public class ResponsibleAreaBll:IResponsibleAreaBll
    {
        private readonly ISResponsibleArea _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<ResponsibleAreaBll> _logger;

        public ResponsibleAreaBll(ISResponsibleArea repository, MessageResponse msgProvider)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
        }

        /// <summary>
        /// Metodo que retorna todas las areas y responsables
        /// </summary>
        /// <returns>Retorna un a colección de tipo ResponsibleArea con todas las areas y responsables</returns>
        public async Task<ServiceResponse> GetResponsibleAreas()
        {
            try
            {
                IEnumerable<ResponsibleArea> data = await _Repository.GetResponsibleAreas();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al consultar la información de las areas responsables" ,ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
