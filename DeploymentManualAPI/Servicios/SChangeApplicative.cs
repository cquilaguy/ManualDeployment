using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public class SChangeApplicative:ISChangeApplicative
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SChangeApplicative> _logger;

        public SChangeApplicative(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<SChangeApplicative> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los aplicativos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo ChangeApplicative con los aplicativos por ChangeID</returns>
        public async Task<IEnumerable<ChangeApplicativeDTO>> GetChangeApplicative(int ChangeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.ChangeApplicative.Where(s => s.ChangeID == ChangeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<ChangeApplicativeDTO> response = _Mapper.Map<IEnumerable<ChangeApplicativeDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los aplicativos con ChangeID: " + ChangeID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite verificar si un changeApplicative existe en la base de datos teniendo en cuenta su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si la información de aplicativo de cambio existe o no</returns>
        public async Task<bool> ChangeApplicativeExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.ChangeApplicative.AnyAsync(x => x.ChangeApplicativeID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un aplicativo por cambio con id" + id, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un aplicativo por cambio
        /// </summary>
        /// <param name="changeApplicative"></param>
        /// <returns>Retorna una JSON con la información de aplicativo por cambio registrada</returns>
        public async Task<ChangeApplicative> PostChangeApplicative(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                ChangeApplicative data = _Mapper.Map<ChangeApplicative>(changeApplicative);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();
                    //Add ingresa la información en la entidad correspondiente
                    dbContext.Add(data);
                    await dbContext.SaveChangesAsync();
                }
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la información de aplicativo por cambio - Data: {@ChangeApplicative}", changeApplicative);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información de un aplicativo por cambio
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un JSON con el aplicativo por cambio actualizado</returns>
        public async Task<ChangeApplicative> PutChangeApplicative(ChangeApplicativeDTO changeApplicative)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y ChangeApplicativeID
                    ChangeApplicative specificChangeApplicative = await dbContext.ChangeApplicative
                        .FirstOrDefaultAsync(s => s.ChangeID == changeApplicative.ChangeID && s.ChangeApplicativeID == changeApplicative.ChangeApplicativeID);

                    if (specificChangeApplicative == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y ContactID proporcionados
                        throw new InvalidOperationException($"No se encontró el registro con ChangeID {changeApplicative.ChangeID} y ChangeApplicativeID {changeApplicative.ChangeApplicativeID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(changeApplicative, specificChangeApplicative);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificChangeApplicative;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la información de cambio por aplicativo - Data: {@ChangeApplicative}", changeApplicative);
                throw ex;
            }
        }
    }
}
