using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Controllers
{
    //Este es el controlador para el endpoint Typology
    [Route("api/[controller]")]
    [ApiController]
    public class TypologyController : ControllerBase
    {
        //Se hace la creación de un objeto ITypologyBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly ITypologyBll _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<TypologyController> _logger;

        public TypologyController(ITypologyBll repository, MessageResponse msgProvider, ILogger<TypologyController> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        ///Metodo que retorna todoslos tipos del formato de capacitación
        /// </summary>
        /// <returns>Retorna una colección de tipo Typology con todos los tipos del formato de capacitación</returns>

        [HttpGet]
        public async Task<IActionResult> GetTypologies() {
            try
            {
                var response = await _Repository.GetTypologies();
                return Ok(response);
            }
            catch(Exception ex) {
                _logger.LogError("Error al obtener todos los tipos del formato de capacitación", ex);
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

    }
}