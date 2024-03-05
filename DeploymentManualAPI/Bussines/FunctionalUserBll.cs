using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class FunctionalUserBll:IFunctionalUserBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISFunctionalUser _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISUser _UserRepository;
        private readonly ILogger<FunctionalUserBll> _logger;




        public FunctionalUserBll(ISFunctionalUser repository, MessageResponse msgProvider, ISChange changeRepository,
            ISUser userRepository, ILogger<FunctionalUserBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _ChangeRepository = changeRepository;
            _UserRepository = userRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener el detalle de usuarios funcionales teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo FunctionalUser con todo el detalle de usuarios funcionales</returns>
        public async Task<ServiceResponse> GetFunctionalUser(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetFunctionalUser(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros de usuarios funcionales con el ChangeID proporcionado en la tabla FunctionalUser.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el detalle de usuarios funcionales con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un detalle de usuario funcional
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna una JSON con el detalle de usuario funcional registrado</returns>
        public async Task<ServiceResponse> PostFunctionalUser(FunctionalUserDTO functionalUser)
        {
            try
            {
                if (await _Repository.FunctionalUserExists(functionalUser.FunctionalUserID))
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Este FunctionalUserID ya se encuentra registrado");
                }

                else
                {
                    if (await _Repository.CheckSequence(functionalUser.ChangeID, functionalUser.Sequence))
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, $"Ya existe una secuencia: {functionalUser.Sequence} para ese FunctionalUserID y ChangeID");
                    }
                    else
                    {
                        FunctionalUser result = await _Repository.PostFunctionalUser(functionalUser);
                        return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de detalle de usuario funcional - Data: {@FunctionalUser}", functionalUser);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un usuario funcional
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna un JSON con la información de usuario funcional actualizado</returns>
        public async Task<ServiceResponse> PutFunctionalUser(FunctionalUserDTO functionalUser)
        {
            try
            {
                if (await _Repository.FunctionalUserExists(functionalUser.FunctionalUserID))
                {
                    FunctionalUser data = await _Repository.PutFunctionalUser(functionalUser);
                    if (data == null)
                    {
                        return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron planes con el ChangeID o FunctionalUserID o secuencia proporcionado en la tabla FunctionalUser.");
                    }
                    else
                    {
                        return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                    }
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Cambio o usuario funcional no existe en la tabla FunctionalUser.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de detalle de usuario funcional - Data: {@PostImplantation}", functionalUser);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna un booleano que indica si existen los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(FunctionalUserDTO functionalUser)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(functionalUser.ChangeID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(functionalUser.UserID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@FunctionalUser}", functionalUser);
                return false;
            }
        }
    }
}
