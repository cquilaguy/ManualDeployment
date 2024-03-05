using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Controllers
{
    //Este es el controlador para el endpoint ChangeApplicative
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeApplicativeController: ControllerBase
    {
        //Se hace la creación de un objeto IContactBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IChangeApplicativeBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<ChangeApplicativeController> _logger;


        public ChangeApplicativeController(IChangeApplicativeBll repository, MessageResponse msgProvider, ILogger<ChangeApplicativeController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que permite obtener la información de aplicativo por cambio
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo ChangeApplicative con aplicativo por cambios registrados</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetChangeApplicative(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetChangeApplicative(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ChangeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la información de aplicativo por cambio con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite ingresar la información de aplicativo por cambio
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna una JSON con la información aplicativo por cambio</returns>
        [HttpPost]
        public async Task<ActionResult> PostChangeApplicative(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                if (changeApplicative == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(changeApplicative))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostChangeApplicative(changeApplicative);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de ChangeApplicative - Data: {@ChangeApplicative}", changeApplicative);
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
                _logger.LogError(ex, "Error al registrar la información de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información general de contactos
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutChangeApplicative(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                if (changeApplicative == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(changeApplicative))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutChangeApplicative(changeApplicative);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con ChangeApplicativeID {changeApplicative.ChangeApplicativeID} y ChangeID {changeApplicative.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
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
                _logger.LogError(ex, "Error al modificar el registro de información de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
