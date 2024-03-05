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
    //Este es el controlador para el endpoint Plan
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController:ControllerBase
    {
        private readonly IPlanBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<PlanController> _logger;


        public PlanController(IPlanBll repository, MessageResponse msgProvider, ILogger<PlanController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        //Llamado del metodo GetPlan teniendo en cuenta el id del cambio y retorno del JSON con lineamientos de arquitectura de AXA


        /// <summary>
        /// Metodo que permite obtener los planes de implementación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo Plan con todos los planes de implementación/returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlan(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetPlan(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ChangeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un plan de implementación
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna una JSON con el plan de implementación registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostPlan(PlanDTO plan)
        {
            try
            {
                if (plan == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(plan))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostPlan(plan);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, wex.Message));

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


                _logger.LogError(ex, "Error al guardar el registro de plan de implementación - Data: {@Plan}", plan);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un plan de implementación
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna un JSON con el plan de implementación actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutPlan(PlanDTO plan)
        {
            try
            {
                if (plan == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(plan))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutPlan(plan);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con PlanID {plan.PlanID} y ChangeID {plan.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
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
                _logger.LogError(ex, "Error al modificar el registro de plan de implementación - Data: {@Plan}", plan);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
