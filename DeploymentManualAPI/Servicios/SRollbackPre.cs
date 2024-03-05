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
    //Hereda de la interfaz SRollbackPre para así implementar los metodo necesarios
    public class SRollbackPre:ISRollbackPre
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SRollbackPre> _logger;

        public SRollbackPre(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory,
            ILogger<SRollbackPre> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los rollback de prerrequisito teniendo en cuenta un PrerequisiteID
        /// </summary>
        /// <param name="PrerequisiteID"></param>
        /// <returns>Retorna una colección de tipo RollbackPre con todos los rollback de prerrequisito</returns>
        public async Task<IEnumerable<RollbackPreDTO>> GetRollbackPre(int PrerequisiteID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.RollbackPre.Where(s => s.PrerequisiteID == PrerequisiteID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<RollbackPreDTO> response = _Mapper.Map<IEnumerable<RollbackPreDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los rollback de prerrequisito con PrerequisiteID: " + PrerequisiteID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un rollback de prerrequisito existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el rollback de prerrequisito existe</returns>
        public async Task<bool> RollbackPreExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.RollbackPre.AnyAsync(x => x.RollbackPreID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un rollback de prerrequisito con id" + id);
                throw ex;
            }
        }


        /// <summary>
        /// Metodo que permite ingresar un rollback de prerrequisito
        /// </summary>
        /// <param name="rollbackPre"></param>
        /// <returns>Retorna una JSON con el rollback de prerrequisito registrado</returns>
        public async Task<RollbackPre> PostRollbackPre(RollbackPreDTO rollbackPre)
        {
            try
            {
                RollbackPre data = _Mapper.Map<RollbackPre>(rollbackPre);
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
                _logger.LogError(ex, "Error al modificar el registro de rollback de prerrequisito - Data: {@RollbackPre}", rollbackPre);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de prerrequisito
        /// </summary>
        /// <param name="rollbackPre"></param>
        /// <returns>Retorna un JSON con el rollback de prerrequisito actualizado</returns>
        public async Task<RollbackPre> PutRollbackPre(RollbackPreDTO rollbackPre)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y PlanID
                    RollbackPre specificRollbackPre = await dbContext.RollbackPre
                        .FirstOrDefaultAsync(s => s.PrerequisiteID == rollbackPre.PrerequisiteID && s.RollbackPreID==rollbackPre.RollbackPreID && s.Sequence==rollbackPre.Sequence);

                    if (specificRollbackPre == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y PlanID y secuencia proporcionados
                        return null;
                        //throw new InvalidOperationException($"No se encontró el registro con ChangeID {plan.ChangeID} y PlanID {plan.PlanID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(rollbackPre, specificRollbackPre);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificRollbackPre;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@RollbackPre}", rollbackPre);
                throw ex;
            }
        }
    }
}
