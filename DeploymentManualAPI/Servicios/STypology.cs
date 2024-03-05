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
    //Hereda de la interfaz ISTypology para así implementar los metodo necesarios
    public class STypology:ISTypology
    {
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<STypology> _logger;

        public STypology(ManualDeploymentContext context, IMapper mapper, ILogger<STypology> logger)
        {
            _context = context;
            _Mapper = mapper;
            _logger = logger;

        }

        /// <summary>
        /// Metodo que devuelve todas los tipos del formato de capacitación
        /// </summary>
        /// <returns>Retorna una colección de tipo Typology con todos los tipos del formato de capacitación registrados </returns>
        public async Task<IEnumerable<Typology>> GetTypologies()
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                IEnumerable<Typology> data = await _context.Typology.ToListAsync();
                IEnumerable<Typology> response = _Mapper.Map<IEnumerable<Typology>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los tipos del formato de capacitación", ex);
                throw ex;
            }
        }
        /// <summary>
        ///Permite que permite verificar si una tipologia existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el tipo existe o no</returns>

        public async Task<bool> TypologyExists(int id)
        {
            bool existe = false;
            try
            {
                // AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Typology.AnyAsync(x => x.TypologyID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un tipo del formato de capacitación con id" + id, ex);
                throw ex;
            }
        }
    }
}
