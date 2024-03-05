using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Controllers
{
    //Este es el controlador para el endpoint FunctionalUser
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalUserController:ControllerBase
    {
        private readonly IFunctionalUserBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<FunctionalUserController> _logger;


        public FunctionalUserController(IFunctionalUserBll repository, MessageResponse msgProvider, ILogger<FunctionalUserController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener el detalle de usuarios funcionales teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo FunctionalUser con todo el detalle de usuarios funcionales</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFunctionalUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetFunctionalUser(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ChangeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el detalle de usuarios funcionales con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un detalle de usuario funcional
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna una JSON con el detalle de usuario funcional registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostFunctionalUser(FunctionalUserDTO functionalUser)
        {
            try
            {
                if (functionalUser == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(functionalUser))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostFunctionalUser(functionalUser);
                            return Ok(result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@FunctionalUser}", functionalUser);
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));

                        }
                    }
                    else
                    {

                        return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "La información enviada no es correcta"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de detalle de usuario funcional - Data: {@FunctionalUser}", functionalUser);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un usuario funcional
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna un JSON con la información de usuario funcional actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutFunctionalUser(FunctionalUserDTO functionalUser)
        {
            try
            {
                if (functionalUser == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(functionalUser))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutFunctionalUser(functionalUser);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con FunctionalUserID {functionalUser.FunctionalUserID} y ChangeID {functionalUser.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@FunctionalUser}", functionalUser);
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, wex.Message));
                        }
                    }
                    else
                    {
                        return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "Error en los datos enviados"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de detalle de usuario funcional - Data: {@FunctionalUser}", functionalUser);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
