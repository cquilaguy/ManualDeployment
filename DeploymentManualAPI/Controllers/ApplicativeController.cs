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

    //Este es el controlador para el endpoint Applicative
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicativeController : ControllerBase
    {
        //Se hace la creación de un objeto IApplicativeBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IApplicativeBll _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<ApplicativeController> _logger;

        public ApplicativeController(IApplicativeBll repository, MessageResponse msgProvider, ILogger<ApplicativeController> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que obtiene todos los aplicativos
        /// </summary>
        /// <returns>Retorna una colección de tipo Applicative con todos los aplicativos registrados</returns>
        [HttpGet]
        public async Task<IActionResult> GetAppplicatives()
        {
            try
            {
                var response = await _Repository.GetApplicatives();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los aplicativos", ex);
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
