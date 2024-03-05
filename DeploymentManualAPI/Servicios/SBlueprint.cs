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
    public class SBlueprint:ISBlueprint
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SBlueprint> _logger;

        public SBlueprint(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory,
            ILogger<SBlueprint> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los blueprints teniendo en cuenta un ApplicativeID
        /// </summary>
        /// <param name="ApplicativeID"></param>
        /// <returns>Retorna una colección de tipo Blueprint con todos los blueprints</returns>
        public async Task<IEnumerable<BlueprintDTO>> GetBlueprint(int ApplicativeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Blueprint.Where(s => s.ApplicativeID == ApplicativeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<BlueprintDTO> response = _Mapper.Map<IEnumerable<BlueprintDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los blueprint con ApplicativeID: " + ApplicativeID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite verificar si un Blueprint existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el blueprint existe</returns>
        public async Task<bool> BlueprintExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Blueprint.AnyAsync(x => x.BlueprintID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un Blueprint con id" + id);
                throw ex;
            }
        }


        /// <summary>
        /// Metodo que permite ingresar un blueprint
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna una JSON con el Blueprint registrado</returns>
        public async Task<Blueprint> PostBlueprint(BlueprintDTO blueprint)
        {
            try
            {
                Blueprint data = _Mapper.Map<Blueprint>(blueprint);
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
                _logger.LogError(ex, "Error al registrar el blueprint - Data: {@Blueprint}", blueprint);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un blueprint
        /// </summary>
        /// <param name="blueprint"></param>
        /// <returns>Retorna un JSON con el blueprint actualizado</returns>
        public async Task<Blueprint> PutBlueprint(BlueprintDTO blueprint)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su blueprintID y ApplicativeID
                    Blueprint specificBlueprint = await dbContext.Blueprint
                        .FirstOrDefaultAsync(s => s.BlueprintID == blueprint.BlueprintID && s.ApplicativeID == blueprint.ApplicativeID);

                    if (specificBlueprint == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el blueprintID y PlanID ApplicativeID
                        return null;
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(blueprint, specificBlueprint);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificBlueprint;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de blueprint- Data: {@Blueprint}", blueprint);
                throw ex;
            }
        }
    }
}
