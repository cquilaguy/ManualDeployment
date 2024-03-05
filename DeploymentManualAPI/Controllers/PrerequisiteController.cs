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
    //Este es el controlador para el endpoint Prerequisite
    [Route("api/[controller]")]
    [ApiController]
    public class PrerequisiteController:ControllerBase
    {
        private readonly IPrerequisiteBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<PrerequisiteController> _logger;


        public PrerequisiteController(IPrerequisiteBll repository, MessageResponse msgProvider, ILogger<PrerequisiteController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que permite obtener los prerrequisitos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna un JSON con la información de los prerrequisitos en especifico</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPrerequisite(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetPrerequisite(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ChangeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los prerrequisitos con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un prerrequisito
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <returns>Retorna una JSON con el prerrequisito registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostPrerequisite(PrerequisiteDTO prerequisite)
        {
            try
            {
                if (prerequisite == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(prerequisite))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostPrerequisite(prerequisite);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@Prerequisite}", prerequisite);
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
                _logger.LogError(ex, "Error al guardar el registro de prerrequisito - Data: {@Prerequisite}", prerequisite);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un prerrequisito
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <returns>Retorna un JSON con el prerrequisito actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutPrerequisite(PrerequisiteDTO prerequisite)
        {
            try
            {
                if (prerequisite == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(prerequisite))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutPrerequisite(prerequisite);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con PrerequisiteID {prerequisite.PrerequisiteID} y ChangeID {prerequisite.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@Prerequisite}", prerequisite);
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, wex.Message));
                        }
                    }
                    else
                    {
                        return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No existe el ChangeID: {prerequisite.ChangeID} en la tabla Prerequisite"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de prerrequisito - Data: {@Prerequisite}", prerequisite);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
