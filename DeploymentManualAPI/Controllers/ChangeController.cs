using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Controllers
{
    //Este es el controlador para el endpoint Change
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeController:ControllerBase
    {
        //Se hace la creación de un objeto IChangeBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly IChangeBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<ChangeController> _logger;
       


        public ChangeController(IChangeBll repository, MessageResponse msgProvider, ILogger<ChangeController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }

        /// <summary>
        /// Metodo que permite obtener un cambio en especifico teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un JSON con la información del cambio en especifico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetChange(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetChange(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se encontraron registros"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cambio con el id" + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite obtener los cambios 
        /// </summary>
        /// <param name="changeNumber"></param>
        /// <returns>Retorna una colección de tipo Plan con todos los planes de implementación/returns>
        [HttpGet("api/changes/{changeNumber}")]
        public async Task<ActionResult> GetChangeS(string changeNumber)
        {
            try
            {
                var result = await _RepositoryBll.GetChangeS(changeNumber);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que retorna la información de un cambio teniendo en cuenta el filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("api/changes/")]
        public async Task<ActionResult> GetFilteredChanges(FilterDTO filter)
        {
            try
            {
                var result = await _RepositoryBll.GetFilteredChanges(filter);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un cambio
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostChange(ChangeDTO change)
        {
            try
            {
                if (change == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(change))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostChange(change);
                            return Ok(result);
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
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@Change}", change);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un cambio
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna un JSON con el cambio actualizad</returns>
        [HttpPut]
        public async Task<ActionResult> PutChange(ChangeDTO change)
        {
            try
            {
                if (change == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(change))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutChange(change);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con ID {change.ChangeID}"));
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
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@Change}", change);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
