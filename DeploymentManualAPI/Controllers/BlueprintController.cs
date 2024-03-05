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
    //Este es el controlador para el endpoint Blueprint
    [Route("api/[controller]")]
    [ApiController]
    public class BlueprintController:ControllerBase
    {
        private readonly IBlueprintBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<BlueprintController> _logger;


        public BlueprintController(IBlueprintBll repository, MessageResponse msgProvider, ILogger<BlueprintController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener los blueprints teniendo en cuenta un ApplicativeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo Blueprint con todos los blueprints</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBlueprint(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetBlueprint(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ApplicativeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los blueprint con ApplicativeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un blueprintn
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna una JSON con el blueprint registrado</returns> 
        [HttpPost]
        public async Task<ActionResult> PostBlueprint(BlueprintDTO blueprint)
        {
            try
            {
                if (blueprint == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(blueprint))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostBlueprint(blueprint);
                            return Ok(result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al validar las llaves foranea del registro de blueprint - Data: {@Blueprint}", blueprint);
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
                _logger.LogError(ex, "Error al ingresar el registro de blueprint - Data: {@Blueprint}", blueprint);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información de un blueprint
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna un JSON con el blueprint actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutBlueprint(BlueprintDTO blueprint)
        {
            try
            {
                if (blueprint == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(blueprint))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutBlueprint(blueprint);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con BlueprintID {blueprint.BlueprintID} y ApplicativeID {blueprint.ApplicativeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de blueprint - Data: {@Blueprint}", blueprint);
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
                _logger.LogError(ex, "Error al modificar el registro de rollback de plan - Data: {@Blueprint}", blueprint);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
