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
    //Hereda de la interfaz ISPrerrequisito para así implementar los metodo necesarios
    public class SPrerequisite:ISPrerequisite
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SPrerequisite> _logger;


        public SPrerequisite(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<SPrerequisite> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener los prerrequisitos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna un JSON con la información del prerrequisito</returns>
        public async Task<IEnumerable<PrerequisiteDTO>> GetPrerequisite(int ChangeID)
        {
            try
            {
                var data = await _context.Prerequisite.Where(s => s.PrerequisiteID == ChangeID  ).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<PrerequisiteDTO> response = _Mapper.Map<IEnumerable<PrerequisiteDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los prerrequisitos con ChangeID: " + ChangeID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un prerrequisito existe en la base de datos teniendo en cuenta su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el prerrequisito existe o no</returns>
        public async Task<bool> PrerequisiteExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Prerequisite.AnyAsync(x => x.PrerequisiteID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un prerrequisito con id" + id);
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

                    // Verificar si ya existe un prerequisito con el mismo Sequence para este ChangeID
                    return await dbContext.Prerequisite.AnyAsync(p => p.ChangeID == ChangeID && p.Sequence == Sequence);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un prerequisito con secuencia:" + Sequence + "y ChangeID: " + ChangeID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un prerrequisito
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        public async Task<Prerequisite> PostPrerequisite(PrerequisiteDTO prerequisite)
        {
            try
            {
                Prerequisite data = _Mapper.Map<Prerequisite>(prerequisite);
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
                _logger.LogError(ex, "Error al guardar el registro de prerrequisito - Data: {@Prerequisite}", prerequisite);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un prerrequisito
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <returns>Retorna un JSON con el prerrequisito actualizado</returns>
        public async Task<Prerequisite> PutPrerequisite(PrerequisiteDTO prerequisite)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y PrerequisiteID
                    Prerequisite specificPrerequisite = await dbContext.Prerequisite
                        .FirstOrDefaultAsync(s => s.ChangeID == prerequisite.ChangeID && s.PrerequisiteID == prerequisite.PrerequisiteID && s.Sequence == prerequisite.Sequence);

                    if (specificPrerequisite == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y PrerequisiteID proporcionados
                        //throw new InvalidOperationException($"No se encontró el registro con ChangeID {prerequisite.ChangeID} y PrerequisiteID {prerequisite.PrerequisiteID}");
                        return null;
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(prerequisite, specificPrerequisite);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificPrerequisite;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de prerequisito - Data: {@Prerequisite}", prerequisite);
                throw ex;
            }
        }

    }
}
