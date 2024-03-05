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
    public class SSignature:ISSignature   
    {
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SSignature> _logger;

        public SSignature(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<SSignature> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;


        }/// <summary>
         /// Metodo que permite obtener una firma en especifico teniendo en cuenta un ChangeID
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Retorna un JSON con la información del cambio en especifico</returns>
        public async Task<IEnumerable<SignatureDTO>> GetSignature(int id)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Signature.Where(s => s.ChangeID == id).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<SignatureDTO> response = _Mapper.Map<IEnumerable<SignatureDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las firmas de aceptación con el changeID: " + id);
                throw ex;
            }
        }
        /// <summary>
        ///Metodo que permite verificar si una firma de aceptación existe en la base de datos teniendo en cuenta su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si el tipo existe o no</returns>
        public async Task<bool> SignatureExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Signature.AnyAsync(x => x.SignatureID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si existe una firma de aceptación con id: " + id);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite ingresar una firma de aceptación 
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna una JSON con el cambio registrado</returns>
        public async Task<Signature> PostSignature(SignatureDTO signature)
        {
            try
            {
                Signature data = _Mapper.Map<Signature>(signature);
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
                _logger.LogError(ex, "Error al guardar el registro de información de firmas de aceptación - Data: {@Signature}", signature);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite actualizar una firma de aceptación
        /// </summary>
        /// <param name="signature"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>

        public async Task<Signature> PutSignature( SignatureDTO signature)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y SignatureID
                    Signature specificSignature = await dbContext.Signature
                        .FirstOrDefaultAsync(s => s.ChangeID == signature.ChangeID && s.SignatureID == signature.SignatureID);

                    if (specificSignature == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y SignatureID proporcionados
                        throw new InvalidOperationException($"No se encontró el registro con ChangeID {signature.ChangeID} y SignatureID {signature.SignatureID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(signature, specificSignature);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificSignature;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la firma de aceptación - Data: {@Signature}", signature);
                throw ex;
                
            }
        }
        /// <summary>
        /// Metodo que guarda la firma de aceptación en la base de datos
        /// </summary>
        /// <returns>Retorna un booleano que indica si la firma de aceptación se guardó o no</returns>
        // 
        public async Task<bool> SaveSignatureAsync()
        {
            try
            {
                int resp = await _context.SaveChangesAsync();
                return (resp >= 0);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al confirmar la firma de aceptación guardada", ex);
                throw ex;
            }
        }
    }
}

