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
    //Este es el controlador para el endpoint Training
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        //Se hace la creación de un objeto ITrainingBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly ITrainingBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<TrainingController> _logger;


        public TrainingController(ITrainingBll repository, MessageResponse msgProvider, ILogger<TrainingController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que permite obtener los formatos de capacitación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo Training con todos los formatos de capacitación </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTraining(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetTraining(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se encontraron registros"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los formatos de capacitación con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un formato de capacitación
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna una JSON con el formato de capacitación registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostTraining(TrainingDTO training)
        {
            try
            {
                if (training == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(training))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostTraining(training);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de formato de capacitación - Data: {@Training}", training);
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
                _logger.LogError(ex, "Error al guardar el registro de formato de capacitación - Data: {@Training}", training);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un formato de capacitación
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutSignature(TrainingDTO training)
        {
            try
            {
                if (training == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(training))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutTraining(training);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con TrainingID {training.TrainingID} y ChangeID {training.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de formato de capacitación - Data: {@Training}", training);
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
                _logger.LogError(ex, "Error al actualizar el registro de formato de capacitación - Data: {@Training}", training);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
