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
    //Hereda de la interfaz ISRequestType para así implementar los metodo necesarios
    public class SRequestType:ISRequestType
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<SRequestType> _logger;
        public SRequestType(ManualDeploymentContext context, IMapper mapper, ILogger<SRequestType> logger)
        {
            _context = context;
            _Mapper = mapper;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que retorna  todos los tipos de solicitudes
        /// </summary>
        /// <returns>Retorna una colección de tipo RequetType con los tipos de solicitud</returns>
        public async Task<IEnumerable<RequestType>> GetRequestTypes()
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                IEnumerable<RequestType> data = await _context.RequestType.ToListAsync();
                IEnumerable<RequestType> response = _Mapper.Map<IEnumerable<RequestType>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener los tipos de solicitudes", ex);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para verificar si un tipo de solicitud existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano indicando si existe o no como registro en la base de datos </returns>
        /// 
        public async Task<bool> RequestTypeExists(int id)
        {
            bool existe = false;
            try
            {
                // AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.RequestType.AnyAsync(x => x.RequestTypeID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un tipo de solicitud con id" + id, ex);
                throw ex;
            }
        }
    }
}
