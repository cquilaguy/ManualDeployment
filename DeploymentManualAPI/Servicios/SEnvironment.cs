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
    public class SEnvironment:ISEnvironment
    {
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<SEnvironment> _logger;
        public SEnvironment(ManualDeploymentContext context, IMapper mapper, ILogger<SEnvironment> logger)
        {
            _context = context;
            _Mapper = mapper;
            _logger = logger;

        }
        /// <summary>
        /// /Metodo que devuelve todos los ambientes
        /// </summary>
        /// <returns>Retorna una colección de tipo Environment con todos los ambientes registrados</returns>
        public async Task<IEnumerable<Repository.Entities.Environment>> GetEnvironments()
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                IEnumerable<Repository.Entities.Environment> data = await _context.Environment.ToListAsync();
                IEnumerable<Repository.Entities.Environment> response = _Mapper.Map<IEnumerable<Repository.Entities.Environment>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los ambientes", ex);
                throw ex;
            }
        }
        /// <summary>
        ///Metodo que permite verificar si un usuario existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el ambiente existe o no</returns>

        public async Task<bool> EnvironmentExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Environment.AnyAsync(x => x.EnvironmentID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un ambiente con id" + id, ex);
                throw ex;
            }
        }
    }
}
