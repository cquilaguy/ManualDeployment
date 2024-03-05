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
    public class ContactBll:IContactBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISContact _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISChange _ChangeRepository;
        private readonly ISUser _UserRepository;
        private readonly ILogger<ContactBll> _logger;


        public ContactBll(ISContact repository, MessageResponse msgProvider, ISChange changeRepository, ISUser userRepository, ILogger<ContactBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _UserRepository = userRepository;
            _ChangeRepository = changeRepository;
            _logger = logger;



        }
        /// <summary>
        /// Metodo que permite obtener la información general de contactos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna un JSON con la información general de contacto en especifico</returns>
        public async Task<ServiceResponse> GetContact(int ChangeID)
        {
            try
            {
                var data = await _Repository.GetContact(ChangeID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontró Información general de contactos con el ChangeID proporcionado en la tabla Contact.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas la información general de contactos con ChangeID: " + ChangeID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar la información general de contactos
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna una JSON con la información general del contacto registrado</returns>
        public async Task<ServiceResponse> PostContact(ContactDTO contact)
        {
            try
            {
                Contact result = await _Repository.PostContact(contact);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de información general del contacto - Data: {@Contact}", contact);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información general de contacto
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un JSON con la información general de contacto</returns>
        public async Task<ServiceResponse> PutContact(ContactDTO contact)
        {
            try
            {
                if (await _Repository.ContactExists(contact.ContactID))
                {
                    Contact data = await _Repository.PutContact(contact);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Información de contacto o cambio no existe en la tabla Signature.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la información general de contacto - Data: {@Contact}", contact);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un booleano que indica si existen o no los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(ContactDTO contact)
        {
            try
            {
                if (!await _ChangeRepository.ChangeExists(contact.ChangeID))
                {
                    return false;
                }
                if (!await _UserRepository.UserExists(contact.UserID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de información general de contacto - Data: {@Change}", contact);
                return false;
            }
        }
    }
}
