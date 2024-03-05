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
    //Este es el controlador para el endpoint RequestType
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTypeController : ControllerBase
    {
        //Se hace la creación de un objeto IRequestTypeBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IRequestTypeBll _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<RequestTypeController> _logger;

        public RequestTypeController(IRequestTypeBll repository, MessageResponse msgProvider, ILogger<RequestTypeController> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que retorna todos los tipos de solicitudes
        /// </summary>
        /// <returns>Retorna una colección de tipo RequestType con todos los tipos de solicitudes registrados</returns>

        [HttpGet]
        public async Task<IActionResult> GetRequestType()
        {
            try
            {
                var response = await _Repository.GetRequestTypes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los tipos de solicitudes", ex);
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
