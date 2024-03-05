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
    public class SRollbackPlan:ISRollbackPlan
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SRollbackPlan> _logger;

        public SRollbackPlan(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory,
            ILogger<SRollbackPlan> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los rollback de plan teniendo en cuenta un PrerequisiteID
        /// </summary>
        /// <param name="PlanID"></param>
        /// <returns>Retorna una colección de tipo RollbackPlan con todos los rollback de plan</returns>
        public async Task<IEnumerable<RollbackPlanDTO>> GetRollbackPlan(int PlanID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.RollbackPlan.Where(s => s.PlanID == PlanID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<RollbackPlanDTO> response = _Mapper.Map<IEnumerable<RollbackPlanDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los rollback de plan con PlanID: " + PlanID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite verificar si un rollback de plan existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el rollback de plan existe</returns>
        public async Task<bool> RollbackPlanExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.RollbackPlan.AnyAsync(x => x.RollbackPlanID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un rollback de plan con id" + id);
                throw ex;
            }
        }


        /// <summary>
        /// Metodo que permite ingresar un rollback de plan
        /// </summary>
        /// <param name="rollbackPlan"></param>
        /// <returns>Retorna una JSON con el rollback de plan registrado</returns>
        public async Task<RollbackPlan> PostRollbackPlan(RollbackPlanDTO rollbackPlan)
        {
            try
            {
                RollbackPlan data = _Mapper.Map<RollbackPlan>(rollbackPlan);
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
                _logger.LogError(ex, "Error al registrar el rollback de plan - Data: {@RollbackPlan}", rollbackPlan);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un rollback de plan
        /// </summary>
        /// <param name="rollbackPlan"></param>
        /// <returns>Retorna un JSON con el rollback de plan actualizado</returns>
        public async Task<RollbackPlan> PutRollbackPlan(RollbackPlanDTO rollbackPlan)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su RollbackPlanID y PlanID
                    RollbackPlan specificRollbackPlan = await dbContext.RollbackPlan
                        .FirstOrDefaultAsync(s => s.PlanID == rollbackPlan.PlanID && s.RollbackPlanID == rollbackPlan.RollbackPlanID && s.Sequence == rollbackPlan.Sequence);

                    if (specificRollbackPlan == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y PlanID y secuencia proporcionados
                        return null;
                        //throw new InvalidOperationException($"No se encontró el registro con ChangeID {plan.ChangeID} y PlanID {plan.PlanID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(rollbackPlan, specificRollbackPlan);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificRollbackPlan;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de rollback plan - Data: {@RollbackPlan}", rollbackPlan);
                throw ex;
            }
        }
    }
}
