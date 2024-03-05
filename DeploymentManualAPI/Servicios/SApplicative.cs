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
    //Hereda de la interfaz ISApplicative para así implementar los metodo necesarios
    public class SApplicative:ISApplicative
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<SApplicative> _logger;
        public SApplicative(ManualDeploymentContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;

        }
        /// <summary>
        /// Metodo que retorna todos los aplicativos
        /// </summary>
        /// <returns>Retorna una colección de tipo Applicative con todos los aplicativos registrados</returns>

        public async Task<IEnumerable<Applicative>> GetApplicatives()
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                IEnumerable<Applicative> data = await _context.Applicative.ToListAsync();
                IEnumerable<Applicative> response = _Mapper.Map<IEnumerable<Applicative>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener todos los aplicativos", ex);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un Aplicativo existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el aplicativo existe</returns>
        public async Task<bool> ApplicativeExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Applicative.AnyAsync(x => x.ApplicativeID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un Aplicativo con id" + id);
                throw ex;
            }
        }
    }
}
