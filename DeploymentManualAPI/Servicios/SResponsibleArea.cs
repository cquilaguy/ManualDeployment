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
    public class SResponsibleArea:ISResponsibleArea
    {
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<SResponsibleArea> _logger;
        public SResponsibleArea(ManualDeploymentContext context, IMapper mapper, ILogger<SResponsibleArea> logger)
        {
            _context = context;
            _Mapper = mapper;
            _logger = logger;

        }
        /// <summary>
        /// /Metodo que retorna todas las areas responsables
        /// </summary>
        /// <returns>retorna una colección de tipo ResponsibleArea con la información de las areas y responsables</returns>

        public async Task<IEnumerable<ResponsibleArea>> GetResponsibleAreas()
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                IEnumerable<ResponsibleArea> data = await _context.ResponsibleArea.ToListAsync();
                IEnumerable<ResponsibleArea> response = _Mapper.Map<IEnumerable<ResponsibleArea>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos las areas y responsables", ex);
                throw ex;
            }
        }

        /// <summary>
        ///Metodo que permite verificar si un usuario existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si existe o no</returns>

        public async Task<bool> ResponsibleAreaExists(int id)
        {
            bool existe = false;
            try
            {
                existe = await _context.ResponsibleArea.AnyAsync(x => x.ResponsibleAreaID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un arearesponsable con id: " + id,ex);
                throw ex;
            }
        }
    }
}
