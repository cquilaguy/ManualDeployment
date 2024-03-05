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
    //Este es el controlador para el endpoint Signature
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController: ControllerBase
    {
        //Se hace la creación de un objeto ISignatureBll el cual contiene la respuesta que se va a generar una vez se haga la petición
        private readonly ISignatureBll _RepositoryBll;
        private readonly MessageResponse _msgProvider;
        private readonly ILogger<SignatureController> _logger;


        public SignatureController(ISignatureBll repository, MessageResponse msgProvider, ILogger<SignatureController> logger)
        {
            _RepositoryBll = repository;
            _msgProvider = msgProvider;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener las firmas de aceptación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un JSON con las firmas de aceptación en especifico</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSignature(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await _RepositoryBll.GetSignature(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se encontraron registros"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las firmas con ChangeID: " + id);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
                
            }
        }

        /// <summary>
        /// Metodo que permite ingresar una firma de aceptación
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna una JSON con la firma de aceptación registrada</returns>
        [HttpPost]
        public async Task<ActionResult> PostSignature(SignatureDTO signature)
        {
            try
            {
                if (signature == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a crear."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(signature))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PostSignature(signature);
                            return Ok(result);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@Change}", signature);
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
                        }
                    }
                    else
                    {
                        return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "Error en los datos enviados"));
                    }
                }
            }
            catch (Exception wex)
            {
                _logger.LogError(wex, "Error al guardar el registro de firma de aceptación - Data: {@Signature}", signature);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, wex.Message));
            }
        }
        /// <summary>
        /// Metodo que permite actualizar una firma de aceptación
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna un JSON con la firma de aceptación actualizada</returns>
        [HttpPut]
        public async Task<ActionResult> PutSignature(SignatureDTO signature)
        {
            try
            {
                if (signature == null)
                {
                    return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "No se envió información del registro a actualizar."));
                }
                else
                {
                    if (await _RepositoryBll.ValidateAsync(signature))
                    {
                        try
                        {
                            var result = await _RepositoryBll.PutSignature(signature);

                            if (result != null)
                            {
                                return Ok(result);
                            }
                            else
                            {
                                // Si result es null, significa que la entidad no se encontró durante la actualización.
                                return NotFound(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"No se encontró la entidad con SignatureID {signature.SignatureID} y ChangeID {signature.ChangeID}"));
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@Signature}", signature);
                            return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
                        }
                    }
                    else
                    {
                        
                        return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, "Error en los datos enviados"));
                    }
                }
            }
            catch (Exception wex)
            {
                _logger.LogError(wex, "Error al modificar el registro de cambio - Data: {@Signature}", signature);
                return BadRequest(_msgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, wex.Message));
            }
        }
    }
}
