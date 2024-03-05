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
    //Este es el controlador para el endpoint RollBackPre
    [Route("api/[controller]")]
    [ApiController]
    public class RollbackPlanController:ControllerBase
    {
        private readonly IRollbackPlanBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<RollbackPlanController> _logger;


        public RollbackPlanController(IRollbackPlanBll repository, MessageResponse msgProvider, ILogger<RollbackPlanController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener los rollback de plan teniendo en cuenta un PlanID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo RollbackPlan con todos los rollback de plan</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRollbackPlan(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetRollbackPlan(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el PlanID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los rollback de plan con PlanID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un rollback de plan
        /// </summary>
        /// <param name="rollbackPlan"></param>
        /// <returns>Retorna una JSON con el rollback de plan registrado</returns> 
        [HttpPost]
        public async Task<ActionResult> PostRollbackPlan(RollbackPlanDTO rollbackPlan)
        {
            try
            {
                if (rollbackPlan == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(rollbackPlan))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostRollbackPlan(rollbackPlan);
                            return Ok(result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al validar las llaves foranea del registro de rollback de plan - Data: {@RollbackPlan}", rollbackPlan);
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
                _logger.LogError(ex, "Error al ingresar el registro de rollback de plan - Data: {@RollbackPlan}", rollbackPlan);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de plan
        /// </summary>
        /// <param name="rollbackPlan"></param>
        /// <returns>Retorna un JSON con el rollback de plan actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutRollbackPlan(RollbackPlanDTO rollbackPlan)
        {
            try
            {
                if (rollbackPlan == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(rollbackPlan))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutRollbackPlan(rollbackPlan);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con RollbackPlanID {rollbackPlan.RollbackPlanID} y PlanID {rollbackPlan.PlanID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de rollback de plan - Data: {@RollbackPlan}", rollbackPlan);
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
                _logger.LogError(ex, "Error al modificar el registro de rollback de plan - Data: {@RollbackPlan}", rollbackPlan);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
