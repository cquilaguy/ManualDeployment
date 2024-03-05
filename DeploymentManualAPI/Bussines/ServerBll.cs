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
    public class ServerBll:IServerBll
    {
        // Se llaman a las interfaces necesarias para obtener los metodos de validación y otras operaciones CRUD
        private readonly ISServer _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ISEnvironment _EnvironmentRepository;
        private readonly ILogger<ServerBll> _logger;



        public ServerBll(ISServer repository, MessageResponse msgProvider, ISEnvironment environmentRepository, ILogger<ServerBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _EnvironmentRepository = environmentRepository;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los servidores teniendo en cuenta un EnvironmentID
        /// </summary>
        /// <param name="EnvironmentID"></param>
        /// <returns>Retorna una colección de tipo Server con todos los servidores registrados</returns>
        public async Task<ServiceResponse> GetServer(int EnvironmentID)
        {
            try
            {
                var data = await _Repository.GetServer(EnvironmentID);

                if (data != null && data.Any())
                {
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {

                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron servidores con el EnvironmentID proporcionado en la tabla Server.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los servidores con EnvironmentID: " + EnvironmentID);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite ingresar un servidor
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna una JSON con el servidor registrado</returns>
        public async Task<ServiceResponse> PostServer(ServerDTO server)
        {
            try
            {
                Server result = await _Repository.PostServer(server);
                return _MsgProvider.MessagesProvider(false, result, (int)HttpStatusCode.Created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el registro de servidor - Data: {@Server}", server);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un servidor
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna un JSON con el servidor actualizado</returns>
        public async Task<ServiceResponse> PutServer(ServerDTO server)
        {
            try
            {
                if (await _Repository.ServerExists(server.ServerID))
                {
                    Server data = await _Repository.PutServer(server);
                    return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);
                }
                else
                {
                    
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "Id de Servidor o Ambiente no existe en la tabla Server.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de servidor - Data: {@Server}", server);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Metodo que valida si los atributos que son llaves existen en la tabla origen
        /// </summary>
        /// <param name="server"></param>
        /// <returns>Retorna un booleano que indica si existen o no los atributos llave foranea</returns>
        public async Task<bool> ValidateAsync(ServerDTO server)
        {
            try
            {
                if (!await _EnvironmentRepository.EnvironmentExists(server.EnvironmentID))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar las llaves foranea del registro de cambio - Data: {@Server}", server);
                return false;
            }
        }
    }
}
