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
    public class TrainingBll:ITrainingBll
    {
        private readonly ISTraining _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISUser _UserRepository;
        private readonly ISType _TypeRepository;
        private readonly ILogger<TrainingBll> _logger;



        public TrainingBll(ISTraining repository, MessageResponse msgProvider, ISChange changeRepository,
            ISUser userRepository, ISType typeRepository, ILogger<TrainingBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _UserRepository = userRepository;
            _ChangeRepository = changeRepository;
            _TypeRepository = typeRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener un formato de capacitación especifico teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo Training con todos los formatos de capacitación registrados</returns>
        public async Task<ServiceResponse> GetTraining(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetTraining(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron firmas con el ChangeID proporcionado en la tabla Training.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los formatos de capacitación con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un formato de capacitación
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna una JSON con el formato de capacitación registrado</returns>
        public async Task<ServiceResponse> PostTraining(TrainingDTO training)
        {
            try
            {
                Training result = await _Repository.PostTraining(training);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de formato de capacitación - Data: {@Training}", training);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información de un formato de capacitación
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna un JSON con el formato de capacitación actualizado</returns>
        public async Task<ServiceResponse> PutTraining(TrainingDTO training)
        {
            try
            {
                if (await _Repository.TrainingExists(training.TrainingID))
                {
                    Training data = await _Repository.PutTraining(training);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Formato de capacitación no existe en la tabla Training.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de formato de capacitación - Data: {@Training}", training);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna un booleano que indica si existen o no los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(TrainingDTO training)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(training.ChangeID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(training.UserID))
                {
                    return false;
                }
                if (!await _TypeRepository.TypeExists(training.TypeID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de formato de capacitación - Data: {@Training}", training);
                return false;
            }
        }

    }
}
