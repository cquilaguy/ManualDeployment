using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SContact que implementa la lógica de negocio 
    /// </summary>

    public interface ISContact
    {
        Task<IEnumerable<ContactDTO>> GetContact(int ChangeID);
        Task<bool> ContactExists(int ChangeID);
        Task<Contact> PostContact(ContactDTO contact);
        Task<Contact> PutContact(ContactDTO contact);
    }
}
