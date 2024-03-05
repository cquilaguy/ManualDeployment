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
    //Este es el controlador para el endpoint ResponsibleArea
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsibleAreaController: ControllerBase
    {
        //Se hace la creación de un objeto IResponsibleAreaBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IResponsibleAreaBll _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<ResponsibleAreaController> _logger;


        public ResponsibleAreaController(IResponsibleAreaBll repository, MessageResponse msgProvider, ILogger<ResponsibleAreaController> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que retorna todas las areasresponsables
        /// </summary>
        /// <returns>Retorna una colección de tipo ResponsibleArea que contiene todas las areas responsables</returns>
        [HttpGet]
        public async Task<IActionResult> GetResponsibleAreas()
        {
            try
            {
                var response = await _Repository.GetResponsibleAreas();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al consultar todos las areasresponsables", ex);
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
