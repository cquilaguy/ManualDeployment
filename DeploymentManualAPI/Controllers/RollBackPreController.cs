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
    //Este es el controlador para el endpoint RollBackPre
    [Route("api/[controller]")]
    [ApiController]
    public class RollBackPreController:ControllerBase
    {
        private readonly IRollbackPreBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<RollBackPreController> _logger;


        public RollBackPreController(IRollbackPreBll repository, MessageResponse msgProvider, ILogger<RollBackPreController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener los rollback de prerrequisito teniendo en cuenta un PrerequisiteID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo RollbackPre con todos los rollback de prerrequisito</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRollbackPre(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetRollbackPre(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el PrerequisiteID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los rollback de prerrequisito con PrerequisiteID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un rollback de prerrequisito
        /// </summary>
        /// <param name="rollbackPre"></param>
        /// <returns>Retorna una JSON con el rollback de prerrequisito registrado</returns> 
        [HttpPost]
        public async Task<ActionResult> PostRollbackPre(RollbackPreDTO rollbackPre)
        {
            try
            {
                if (rollbackPre == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(rollbackPre))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostRollbackPre(rollbackPre);
                            return Ok(result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al validar las llaves foranea del registro de rollback de prerrequisito - Data: {@RollbackPre}", rollbackPre);
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
                _logger.LogError(ex, "Error al ingresar el registro de rollback de prerrequisito - Data: {@RollbackPre}", rollbackPre);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de prerrequisito
        /// </summary>
        /// <param name="rollbackPre"></param>
        /// <returns>Retorna un JSON con el rollback de prerrequisito actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutRollbackPre(RollbackPreDTO rollbackPre)
        {
            try
            {
                if (rollbackPre == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(rollbackPre))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutRollbackPre(rollbackPre);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con RollbackPreID {rollbackPre.RollbackPreID} y PrerequisiteID {rollbackPre.PrerequisiteID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de rollback de prerrequisito - Data: {@RollbackPre}", rollbackPre);
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
                _logger.LogError(ex, "Error al modificar el registro de rollback de prerrequisito - Data: {@RollbackPre}", rollbackPre);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

    }
}
