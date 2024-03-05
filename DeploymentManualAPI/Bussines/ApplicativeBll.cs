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
    public class ApplicativeBll : IApplicativeBll
    {
        private readonly ISApplicative _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<ApplicativeBll> _logger;
        public ApplicativeBll(ISApplicative repository, MessageResponse msgProvider, ILogger<ApplicativeBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que retorna todos los aplicativos
        /// </summary>
        /// <returns>Retorna una colección de tipo Applicative con todos los aplicativos </returns>
        public async Task<ServiceResponse> GetApplicatives()
        {
            try
            {
                IEnumerable<Applicative> data = await _Repository.GetApplicatives();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los aplicativos", ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
