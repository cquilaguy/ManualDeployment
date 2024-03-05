using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Controllers
{
    //Este es el controlador para el endpoint Environment
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController: ControllerBase
    {
        private readonly IEnvironmentBll _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<EnvironmentController> _logger;

        public EnvironmentController(IEnvironmentBll repository, MessageResponse msgProvider, ILogger<EnvironmentController> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que ovtiene todos los ambientes
        /// </summary>
        /// <returns>Retorna una colección de tipo Environment con todos los ambientes registrados</returns>
        [HttpGet]
        public async Task<IActionResult> GetEnvironments()
        {
            try
            {
                var response = await _Repository.GetEnvironments();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los ambientes ", ex);
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
