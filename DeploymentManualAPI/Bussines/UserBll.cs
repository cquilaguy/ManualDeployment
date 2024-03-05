using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class UserBll: IUserBll
    {

        private readonly ISUser _Repository;
        private readonly MessageResponse _MsgProvider;
        private readonly ILogger<UserBll> _logger;
        public UserBll(ISUser repository, MessageResponse msgProvider, ILogger<UserBll> logger)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que retorna todos los usuarios registrados
        /// </summary>
        /// <returns>retorna una colección de tipo User con la información de los usuarios registrados</returns>
        public async Task<ServiceResponse> GetUsers()
        {
            try
            {
                //Se llama al metodo Get de la clase ISUser y adicionalmente se hacen validaciones 
                IEnumerable<User> data = await _Repository.GetUsers();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los usuarios registrados en la base de datos",ex);
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
