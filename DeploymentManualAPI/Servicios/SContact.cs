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
    //Hereda de la interfaz ISContact para así implementar los metodo necesarios
    public class SContact:ISContact
    {
        //Llamado al contexto para poder usar las entidades gracias a Entity Framework Core
        private readonly ManualDeploymentContext _context;
        private readonly IMapper _Mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<SContact> _logger;

        public SContact(ManualDeploymentContext context, IMapper mapper, IServiceScopeFactory serviceScopeFactory, ILogger<SContact> logger)
        {
            _context = context;
            _Mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }
        /// <summary>
        /// Metodo que permite obtener la información general de contactos teniendo en cuenta un ChangeID
        /// </summary>
        /// <param name="ChangeID"></param>
        /// <returns>Retorna una colección de tipo Contact con toda la información general de contactos</returns>
        public async Task<IEnumerable<ContactDTO>> GetContact(int ChangeID)
        {
            try
            {
                //ToListAsync convierte todos los objetos de la entidad en una lista
                var data = await _context.Contact.Where(s => s.ChangeID == ChangeID).ToListAsync();
                //Mapea de una entidad a un JSON
                IEnumerable<ContactDTO> response = _Mapper.Map<IEnumerable<ContactDTO>>(data);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener toda la información general de contactos con ChangeID: " + ChangeID);
                throw ex;
            }
        }
        /// <summary>
        /// Metodo que permite verificar si la información general de contacto existe en la base de datos teniendo en cuenta su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un booleano que indica si la información general de contacto existe o no</returns>
        public async Task<bool> ContactExists(int id)
        {
            bool existe = false;
            try
            {
                //AnyAsync busca un registro en la base de datos teniendo en cuenta una condición
                existe = await _context.Contact.AnyAsync(x => x.ContactID == id);
                return existe;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al verificar si existe la información general de un contacto con id" + id, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite ingresar la información general de un contacto
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna una JSON con la información general de un contacto registrada</returns>
        public async Task<Contact> PostContact(ContactDTO contact)
        {
            try
            {
                Contact data = _Mapper.Map<Contact>(contact);
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
                _logger.LogError(ex, "Error al guardar la información general de un contacto - Data: {@Contact}", contact);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo que permite actualizar la información general de un contacto registrado
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Retorna un JSON con el cambio actualizado</returns>
        public async Task<Contact> PutContact(ContactDTO contact)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<ManualDeploymentContext>();

                    // Buscar el registro específico por su ChangeID y ContactID
                    Contact specificContact = await dbContext.Contact
                        .FirstOrDefaultAsync(s => s.ChangeID == contact.ChangeID && s.ContactID == contact.ContactID);

                    if (specificContact == null)
                    {
                        // Manejar el caso en que no se encontró el registro con el ChangeID y ContactID proporcionados
                        throw new InvalidOperationException($"No se encontró el registro con ChangeID {contact.ChangeID} y ContactID {contact.ContactID}");
                    }

                    // Mapear las propiedades actualizadas del DTO al registro existente
                    _Mapper.Map(contact, specificContact);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();

                    return specificContact;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la información general de un contacto - Data: {@Contact}", contact);
                throw ex;
            }
        }

    }
}
