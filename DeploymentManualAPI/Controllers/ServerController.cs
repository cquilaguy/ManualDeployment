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
    //Este es el controlador para el endpoint Server
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly IServerBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<ServerController> _logger;


        public ServerController(IServerBll repository, MessageResponse msgProvider, ILogger<ServerController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener las firmas de aceptación teniendo en cuenta un EnviromentID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo Server con todos los servidores registrados</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult> GetServer(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetServer(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el EnvironmentID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los servidores con EnvironmentID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un servidor
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna una JSON con el servidor registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostServer(ServerDTO server)
        {
            try
            {
                if (server == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(server))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostServer(server);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@Server}", server);
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
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@Server}", server);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información de un servidor
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna un JSON con el servidor actualizado</returns>
        [HttpPut]
        public async Task<ActionResult> PutServer(ServerDTO server)
        {
            try
            {
                if (server == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(server))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutServer(server);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con ServerID {server.ServerID} y EnvironmentID {server.EnvironmentID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de cambio - Data: {@Server}", server);
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
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@Server}", server);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
