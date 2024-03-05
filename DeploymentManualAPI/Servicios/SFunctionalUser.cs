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
    //Hereda de la interfaz ISFunctionalUser para así implementar los metodo necesarios
    public class SFunctionalUser:ISFunctionalUser
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SFunctionalUser> _logger;

        public SFunctionalUser(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory,
            ILogger<SFunctionalUser> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }

        /// <summary>
        /// Metodo que permite obtener el detalle de usuarios funcionales teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo FunctionalUser con todo el detalle de usuarios funcionales</returns>
        public async Task<IEnumerable<FunctionalUserDTO>> GetFunctionalUser(int ChangeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.FunctionalUser.Where(s => s.ChangeID == ChangeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<FunctionalUserDTO> response = _Mapper.Map<IEnumerable<FunctionalUserDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el detalle de usuarios funcionales con ChangeID: " + ChangeID);
                throw ex;
            }
        }
        /// <summary>
        /// _logger.LogError(ex, "Error al verificar si existe un detalle de usuario funcional con id" + id);
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el usuario funcional existe</returns>
        public async Task<bool> FunctionalUserExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.FunctionalUser.AnyAsync(x => x.FunctionalUserID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un usuario funcional con id" + id);
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
                    return await dbContext.FunctionalUser.AnyAsync(p => p.ChangeID == ChangeID && p.Sequence == Sequence);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un detalle de usuario funcional con secuencia:" + Sequence + "y ChangeID: " + ChangeID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un detalle de usuario funcional
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna una JSON con el detalle de usuario funcional registrado</returns>
        public async Task<FunctionalUser> PostFunctionalUser(FunctionalUserDTO functionalUser)
        {
            try
            {
                FunctionalUser data = _Mapper.Map<FunctionalUser>(functionalUser);
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
                _logger.LogError(ex, "Error al guardar el registro de detalle de usuario funcional - Data: {@FunctionalUser}", functionalUser);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un usuario funcional
        /// </summary>
        /// <param name="functionalUser"></param>
        /// <returns>Retorna un JSON con la información de usuario funcional actualizado</returns>
        public async Task<FunctionalUser> PutFunctionalUser(FunctionalUserDTO functionalUser)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y PlanID
                    FunctionalUser specificFunctionalUser = await dbContext.FunctionalUser
                        .FirstOrDefaultAsync(s => s.ChangeID == functionalUser.ChangeID && s.FunctionalUserID == functionalUser.FunctionalUserID && s.Sequence == functionalUser.Sequence);

                    if (specificFunctionalUser == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y PlanID proporcionados
                        return null;
                        //throw new InvalidOperationException($"No se encontró el registro con ChangeID {plan.ChangeID} y PlanID {plan.PlanID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(functionalUser, specificFunctionalUser);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificFunctionalUser;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de detalle de usuario funcional - Data: {@PostImplantation}", functionalUser);
                throw ex;
            }
        }
    }
}
