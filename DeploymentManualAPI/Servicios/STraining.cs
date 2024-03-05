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
    public class STraining:ISTraining
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<STraining> _logger;

        public STraining(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<STraining> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener las formatos de capacitación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo Training con todos los formatos de capacitación registrados</returns>
        public async Task<IEnumerable<TrainingDTO>> GetTraining(int ChangeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Training.Where(s => s.ChangeID == ChangeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<TrainingDTO> response = _Mapper.Map<IEnumerable<TrainingDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los formatos de capacitación para con ChangeID: " + ChangeID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un formato de capacitación existe en la base de datos teniendo en cuenta su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el tipo existe o no</returns>
        public async Task<bool> TrainingExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Training.AnyAsync(x => x.TrainingID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe un formato de capacitación con id" + id, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un formato de capacitación
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        public async Task<Training> PostTraining(TrainingDTO training)
        {
            try
            {
                Training data = _Mapper.Map<Training>(training);
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
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@Training}", training);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar un formato de capacitación
        /// </summary>
        /// <param name="training"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        public async Task<Training> PutTraining(TrainingDTO training)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y TrainingID
                    Training specificTraining = await dbContext.Training
                        .FirstOrDefaultAsync(s => s.ChangeID == training.ChangeID && s.TrainingID == training.TrainingID);

                    if (specificTraining == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y TrainingID proporcionados
                        throw new InvalidOperationException($"No se encontró el registro con ChangeID {training.ChangeID} y TrainingID {training.TrainingID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(training, specificTraining);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificTraining;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de cambio - Data: {@Training}", training);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que confirma que los formatos de capacitación fueron realizados
        /// </summary>
        /// <returns>Retorna un booleano que indica si el formato de capacitación se guardó o no</returns>
        public async Task<bool> SaveTrainingAsync()
        {
            try
            {
                int resp = await _context.SaveChangesAsync();
                return (resp >= 0);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al confirmar el cambio guardado", ex);
                throw ex;
            }
        }


    }
}
