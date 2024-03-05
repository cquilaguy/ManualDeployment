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
    //Hereda de la interfaz ISPlan para así implementar los metodo necesarios
    public class SPostImplantation:ISPostImplantation
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SPostImplantation> _logger;

        public SPostImplantation(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<SPostImplantation> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        /// <summary>
        /// Metodo que permite obtener los prerrequisitos postimplantación teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo PostImplantation con todos los prerrequisitos postimplantación</returns>
        public async Task<IEnumerable<PostImplantationDTO>> GetPostImplantation(int ChangeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Postimplantation.Where(s => s.ChangeID == ChangeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<PostImplantationDTO> response = _Mapper.Map<IEnumerable<PostImplantationDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas los prerrequisitos postimplantación con ChangeID: " + ChangeID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si un prerrequisito postimplantación existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el prerequisito postimplantacion existe</returns>
        public async Task<bool> PostImplantationExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Postimplantation.AnyAsync(x => x.PostimplantationID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un prerrequisito postimplantación con id" + id);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si ya existe un atributo con esa secuencia y ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <param name="Sequence"></param>
        /// <returns>Retorna un booleano que indica si la secuencia existe<</returns>
        public async Task<bool> CheckSequence(int ChangeID, int Sequence)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Verificar si ya existe un prerequisito con el mismo Sequence para este ChangeID
                    return await dbContext.Postimplantation.AnyAsync(p => p.ChangeID == ChangeID && p.Sequence == Sequence);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe un prerrequisito postimplantación con secuencia:" + Sequence + "y ChangeID: " + ChangeID);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar un prerrequisito postimplantación
        /// </summary>
        /// <param name="postImplantation"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        public async Task<Postimplantation> PostPostImplantation(PostImplantationDTO postImplantation)
        {
            try
            {
                Postimplantation data = _Mapper.Map<Postimplantation>(postImplantation);
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
                _logger.LogError(ex, "Error al guardar el registro de cambio - Data: {@PostImplantation}", postImplantation);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar la información de un prerrequisito postimplantación
        /// </summary>
        /// <param name="postImplantation"></param>
        /// <returns>Retorna un JSON con el prerrequisito postimplantación actualizado</returns>
        public async Task<Postimplantation> PutPostImplantation(PostImplantationDTO postImplantation)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y PostimplantationID
                    Postimplantation specificPostImplantation = await dbContext.Postimplantation
                        .FirstOrDefaultAsync(s => s.ChangeID == postImplantation.ChangeID && s.ChangeID == postImplantation.PostimplantationID && s.Sequence ==postImplantation.Sequence);

                    if (specificPostImplantation == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y PostImplantationID y secuencia proporcionados
                        return null;
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(postImplantation, specificPostImplantation);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificPostImplantation;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el registro de prerrequisito postimplantación - Data: {@PostImplantation}", postImplantation);
                throw ex;
            }
        }
    }
}
