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
    //Este es el controlador para el endpoint User
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Se hace la creación de un objeto IUserBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IUserBll _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBll repository, MessageResponse msgProvider, ILogger<UserController> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        ///Metodo que retorna todos los usuarios registrados
        /// </summary>
        /// <returns> Retorna una colección de tipo User con la información de los usuarios registrados</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _Repository.GetUsers();
                return Ok(response);
            }
            catch (Exception ex){
                _logger.LogError("Error al obtener todos los usuarios registrados en la base de datos", ex);
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
