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
    //Este es el controlador para el endpoint Contact
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController:ControllerBase
    {
        //Se hace la creación de un objeto IContactBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IContactBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<ContactController> _logger;


        public ContactController(IContactBll repository, MessageResponse msgProvider, ILogger<ContactController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que permite obtener la información general de contactos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo Contact con información general de contactos registrados</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetContact(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetContact(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ChangeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas la información general de contactos con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite ingresar la información general de un contacto
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna una JSON con la información general de un contacto registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostContact(ContactDTO contact)
        {
            try
            {
                if (contact == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(contact))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostContact(contact);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@Contact}", contact);
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
                _logger.LogError(ex, "Error al registrar la información general de contacto - Data: {@Contact}", contact);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información general de contactos
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutContact(ContactDTO contact)
        {
            try
            {
                if (contact == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(contact))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutContact(contact);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con ContactID {contact.ContactID} y ChangeID {contact.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@Contact}", contact);
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
                _logger.LogError(ex, "Error al modificar el registro de información general de contacto - Data: {@Contact}", contact);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

    }
}
