using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using Microsoft.AspNetCore.Http;
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
    public class PostImplantationController : ControllerBase
    {
        private readonly IPostImplantationBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<PostImplantationController> _logger;


        public PostImplantationController(IPostImplantationBll repository, MessageResponse msgProvider, ILogger<PostImplantationController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener los prerrequisitos postimplantación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna una colección de tipo PostImplantation con todos los prerrequisitos postimplantación</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPostImplantation(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetPostImplantation(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No se encontraron registros para el ChangeID: {id}"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los prerrequisitos postimplantación con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un prerrequisito postimplantación
        /// </summary>
        /// <param name="postImplantation"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        [HttpPost]
        public async Task<ActionResult> PostPostImplantation(PostImplantationDTO postImplantation)
        {
            try
            {
                if (postImplantation == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(postImplantation))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostPostImplantation(postImplantation);
                            return Ok(result);
                        }
                        catch (Exception wex)
                        {
                            _logger.LogError(wex, "Error al validar las llaves foranea del registro de prerrequisito postimplantación - Data: {@PostImplantation}", postImplantation);
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


                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@PostImplantation}", postImplantation);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
        //Llamado del metodo PutPostImplantation y retorno del JSON con lineamientos de arquitectura de AXA
        [HttpPut]
        public async Task<ActionResult> PutPostImplantation(PostImplantationDTO postImplantation)
        {
            try
            {
                if (postImplantation == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(postImplantation))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutPostImplantation(postImplantation);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con PostimplantationID {postImplantation.PostimplantationID} y ChangeID {postImplantation.ChangeID}"));
                            }
                        }
                        catch (Exception wex)
                        {
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, wex.Message));
                        }
                    }
                    else
                    {
                        return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, $"No existe el ChangeID: {postImplantation.ChangeID} en la tabla Postimplantation"));
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
