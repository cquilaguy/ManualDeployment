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
    public class ChangeBll:IChangeBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISChange _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISUser _UserRepository;
        private readonly ISEnvironment _EnvironmentRepository;
        private readonly ISRequestType _RequestTypeRepository;
        private readonly ISTypology _TypologyRepository;
        private readonly ISStatus _StatusRepository;
        private readonly ILogger<ChangeBll> _logger;

        public ChangeBll(ISChange repository, MessageResponse msgProvider, ISUser userRepository, ISEnvironment environmentRepository,
            ISRequestType requestTypeRepository,ISTypology typologyRepository, ISStatus statusRepository, ILogger<ChangeBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _UserRepository = userRepository;
            _EnvironmentRepository = environmentRepository;
            _RequestTypeRepository = requestTypeRepository;
            _TypologyRepository = typologyRepository;
            _StatusRepository = statusRepository;

            _logger = logger;


        }
        /// <summary>
        /// Metodo que permite obtener un cambio en especifico teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un JSON con la información del cambio en especifico</returns>
        public async Task<ServiceResponse> GetChange (int id)
        {
            try
            {
                if (await _Repository.ChangeExists(id))
                {
                    ChangeDTO data = await _Repository.GetChange(id);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Cambio no existe en la tabla Change.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cambio con el id" + id);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite obtener un cambio en especifico teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un JSON con la información del cambio en especifico</returns>
        public async Task<ServiceResponse> GetChangeS(string changeNumber)
        {
            try
            {
                ChangeObj data = await _Repository.GetChangeS(changeNumber);
                if (data == null)
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron planes con el changeNumber proporcionado en la tabla Change.");
                }
                else {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cambio con el changeNumber" + changeNumber);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite registrar un cambio
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        public async Task<ServiceResponse> PostChange(ChangeDTO change)
        {
            try
            {
                Change result = await _Repository.PostChange(change);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@Change}", change);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un cambio
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        public async Task<ServiceResponse> PutChange(ChangeDTO change)
        {
            try
            {

                if (await _Repository.ChangeExists(change.ChangeID))
                {
                    Change data = await _Repository.PutChange(change);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Cambio no existe en la tabla Change.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@Change}", change);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="change"></param>
        /// <returns>Retorna un booleano que indica si existen o no los atributos ´llave foranea</returns>
        public async Task<bool> ValidateAsync(ChangeDTO change)
        {
            try
            {
                if (!await _UserRepository.UserExists(change.UserID))
                {
                    return false;
                }
                if (!await _EnvironmentRepository.EnvironmentExists(change.EnvironmentID))
                {
                    return false;
                }
                if (!await _RequestTypeRepository.RequestTypeExists(change.RequestTypeID))
                {
                    return false;
                }
                if (!await _TypologyRepository.TypologyExists(change.TypologyID))
                {
                    return false;
                }
                if (!await _StatusRepository.StatusExists(change.StatusID))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@Change}", change);
                return false;
            }
        }
        public async Task<ServiceResponse> GetFilteredChanges(FilterDTO filter)
        {
            try
            {
                FilterObj data = await _Repository.GetFilteredChanges(filter);
                if (data == null)
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron cambios con la información proporcionada");
                }
                else
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cambio con los datos" + filter);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
