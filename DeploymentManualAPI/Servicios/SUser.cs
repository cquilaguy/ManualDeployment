using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    //La presente clase hereda de la Interfaz ISUser
    public class SUser: ISUser
    {
        
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<SUser> _logger;


        public SUser(ManualDeploymentContext context, IMapper mapper, ILogger<SUser> logger)
        {
            _context = context;
            _Mapper = mapper;
            _logger = logger;

        }
        /// <summary>
        /// //Metodo GET que devuelve todos los usuarios creados en la base de datos
        /// </summary>
        /// <returns>IEnumerable<User></returns>

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                IEnumerable<User> data = await _context.User.ToListAsync();
                IEnumerable<User> response = _Mapper.Map<IEnumerable<User>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los usuarios" , ex);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un usuario existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<bool> UserExists(int id)
        {
            bool existe = false;
            try
            {
                existe = await _context.User.AnyAsync(x => x.UserID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe el usuario con el id" + id, ex);
                throw ex;
            }
        }
    }
}
