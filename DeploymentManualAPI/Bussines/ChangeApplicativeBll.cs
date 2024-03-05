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
    public class ChangeApplicativeBll: IChangeApplicativeBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISChangeApplicative _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISApplicative _ApplicativeRepository;
        private readonly ILogger<ChangeApplicativeBll> _logger;


        public ChangeApplicativeBll(ISChangeApplicative repository, MessageResponse msgProvider, ISChange changeRepository, ISApplicative applicativeRepository, ILogger<ChangeApplicativeBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _ApplicativeRepository = applicativeRepository;
            _ChangeRepository = changeRepository;
            _logger = logger;



        }
        /// <summary>
        /// Metodo que permite obtener la información de cambio por aplicativo segun un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna un JSON con la información general de contacto en especifico</returns>
        public async Task<ServiceResponse> GetChangeApplicative(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetChangeApplicative(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontró aplicativo por cambio con el ChangeID proporcionado en la tabla ChangeApplicative.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los aplicativos por cambio con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar la información de aplicativo por cambio
        /// </summary>
        /// <param name="changeApplicative"></param>
        /// <returns>Retorna una JSON con la información de aplicativo por cambio</returns>
        public async Task<ServiceResponse> PostChangeApplicative(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                ChangeApplicative result = await _Repository.PostChangeApplicative(changeApplicative);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de información de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de aplicativo por cambio
        /// </summary>
        /// <param name="changeApplicative"></param>
        /// <returns>Retorna un JSON con la información de aplicativo por cambio/returns>
        public async Task<ServiceResponse> PutChangeApplicative(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                if (await _Repository.ChangeApplicativeExists(changeApplicative.ChangeApplicativeID))
                {
                    ChangeApplicative data = await _Repository.PutChangeApplicative(changeApplicative);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Información de aplicativo por no existe en la tabla ChangeApplicative.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la información de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un booleano que indica si existen o no los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(changeApplicative.ChangeID))
                {
                    return false;
                }
                if (!await _ApplicativeRepository.ApplicativeExists(changeApplicative.ApplicativeID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
                return false;
            }
        }
    }
}
