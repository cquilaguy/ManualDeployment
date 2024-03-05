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
    public class SPlan:ISPlan
    {
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SPlan> _logger;

        public SPlan(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;

        }
        /// <summary>
        /// Metodo que permite obtener los planes de implementación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo Plan con todos los planes de implementación/returns>
        public async Task<IEnumerable<PlanDTO>> GetPlan(int ChangeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Plan.Where(s => s.ChangeID == ChangeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<PlanDTO> response = _Mapper.Map<IEnumerable<PlanDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos planes con ChangeID: " + ChangeID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un plan de implementación existe teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el plan de implementación existe</returns>
        public async Task<bool> PlanExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Plan.AnyAsync(x => x.PlanID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un plan con id" + id);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si ya existe un atributo con esa secuencia y ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <param name="Sequence"></param>
        /// <returns>Retorna un booleano que indica si la secuencia existe</returns>
        public async Task<bool> CheckSequence(int ChangeID, int Sequence)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Verificar si ya existe un plan con el mismo Sequence para este ChangeID
                    return await dbContext.Plan.AnyAsync(p => p.ChangeID == ChangeID && p.Sequence == Sequence);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un plan de implementación con secuencia:" + Sequence + "y ChangeID: " + ChangeID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un plan de implementación
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna una JSON con el plan de implementación registrado</returns>
        public async Task<Plan> PostPlan(PlanDTO plan)
        {
            try
            {
                Plan data = _Mapper.Map<Plan>(plan);
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
                _logger.LogError(ex, "Error al guardar el registro de plan de implementación - Data: {@Plan}", plan);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un plan de implementación
        /// </summary>
        /// <param name="plan"></param>
        /// <returns>Retorna un JSON con el plan de implementación actualizado</returns>
        public async Task<Plan> PutPlan(PlanDTO plan)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y PlanID
                    Plan specificPlan = await dbContext.Plan
                        .FirstOrDefaultAsync(s => s.ChangeID == plan.ChangeID && s.PlanID == plan.PlanID && s.Sequence==plan.Sequence );

                    if (specificPlan == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y PlanID proporcionados
                        return null;
                        //throw new InvalidOperationException($"No se encontró el registro con ChangeID {plan.ChangeID} y PlanID {plan.PlanID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(plan, specificPlan);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificPlan;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de plan de implementación - Data: {@Plan}", plan);
                throw ex;
            }
        }
    }
}
