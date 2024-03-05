using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class PostImplantationBll:IPostImplantationBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISPostImplantation _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISUser _UserRepository;
        private readonly ILogger<PostImplantationBll> _logger;




        public PostImplantationBll(ISPostImplantation repository, MessageResponse msgProvider, ISChange changeRepository,
            ISUser UserRepository, ILogger<PostImplantationBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _ChangeRepository = changeRepository;
            _UserRepository = UserRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los prerrequisitos postimplantación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo PostImplantation con todos los prerrequisitos postimplantación</returns>
        public async Task<ServiceResponse> GetPostImplantation(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetPostImplantation(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron Post implantaciones con el ChangeID proporcionado en la tabla Postimplantation.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los prerrequisitos postimplantación con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un prerrequisito postimplantación
        /// </summary>
        /// <param name="postImplantation"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        public async Task<ServiceResponse> PostPostImplantation(PostImplantationDTO postImplantation)
        {
            try
            {
                if (await _Repository.PostImplantationExists(postImplantation.PostimplantationID))
                {

                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"El PostimplantationID: {postImplantation.PostimplantationID} ya se encuentra registrado");
                }

                else
                {
                    if (await _Repository.CheckSequence(postImplantation.ChangeID, postImplantation.Sequence))
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"Ya existe una secuencia: {postImplantation.Sequence} para ese PostImplantationID y ChangeID");
                    }
                    else
                    {
                        Postimplantation result = await _Repository.PostPostImplantation(postImplantation);
                        return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@PostImplantation}", postImplantation);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un prerrequisito postimplantación
        /// </summary>
        /// <param name="postImplantation"></param>
        /// <returns>Retorna un JSON con el prerrequisito postimplantación actualizado</returns>
        public async Task<ServiceResponse> PutPostImplantation(PostImplantationDTO postImplantation)
        {
            try
            {
                if (await _Repository.PostImplantationExists(postImplantation.PostimplantationID))
                {
                    Postimplantation data = await _Repository.PutPostImplantation(postImplantation);
                    if (data == null)
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron planes con el ChangeID o PostImplantationID o secuencia proporcionado en la tabla PostImplantation.");
                    }
                    else
                    {
                        return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                    }
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Cambio o Postimplantacion no existe en la tabla Postimplantation.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de prerrequisito postimplantación - Data: {@PostImplantation}", postImplantation);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="postImplantation"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(PostImplantationDTO postImplantation)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(postImplantation.ChangeID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(postImplantation.UserID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@PostImplantation}", postImplantation);
                return false;
            }
        }
    }
}
