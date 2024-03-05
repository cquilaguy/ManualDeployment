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
    public class SignatureBll: ISignatureBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISSignature _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISUser _UserRepository;
        private readonly ILogger<SignatureBll> _logger;


        public SignatureBll(ISSignature repository, MessageResponse msgProvider, ISChange changeRepository , ISUser userRepository,
            ILogger<SignatureBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _UserRepository = userRepository;
            _ChangeRepository = changeRepository;
            _logger = logger;



        }
        /// <summary>
        ///Metodo que permite obtener las firmas de aceptación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna un JSON con la información de las firmas de aceptacíón en especifico</returns>
        public async Task<ServiceResponse> GetSignature(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetSignature(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron firmas con el ChangeID proporcionado en la tabla Signature.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todas las firmas con ChangeID: " + ChangeID, ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        ///Metodo que permite ingresar una firma de aceptación
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna una JSON con la firma de aceptación registrada</returns>
        public async Task<ServiceResponse> PostSignature(SignatureDTO signature)
        {
            try
            {
                Signature result = await _Repository.PostSignature(signature);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de firmas de aceptación - Data: {@Signature}", signature);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de una firma de aceptación
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna un JSON con la firma de aceptación actualizada</returns>

        public async Task<ServiceResponse> PutSignature(SignatureDTO signature)
        {
            try
            {
                if (await _Repository.SignatureExists(signature.SignatureID))
                {
                    Signature data = await _Repository.PutSignature(signature);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Firma no existe en la tabla Signature.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@Signature}", signature);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna un booleano que indica si existen o no los atributos llave foranea</returns>

        public async Task<bool> ValidateAsync(SignatureDTO signature)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(signature.ChangeID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(signature.UserID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(signature.TrainedUserID))
                {
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de firma de aceptación - Data: {@Signature}", signature);

                return false;
            }
        }

    }
}
