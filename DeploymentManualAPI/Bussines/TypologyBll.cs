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
    public class TypologyBll: ITypologyBll
    {
        private readonly ISTypology _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<TypologyBll> _logger;
        public TypologyBll(ISTypology repository, MessageResponse msgProvider, ILogger<TypologyBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que retorna todos los tipos del formato de capacitación
        /// </summary>
        /// <returns>Retorna una colección de tipo Typology con todos los tipos del formato de capacitación</returns>
        public async Task<ServiceResponse> GetTypologies()
        {
            try
            {
                IEnumerable<Typology> data = await _Repository.GetTypologies();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los tipos del formato de capacitación", ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
