using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por ContactBll donde se da la respuesta JSON 
    /// </summary>

    public interface IContactBll
    {
        Task<ServiceResponse> GetContact(int ChangeID);
        Task<ServiceResponse> PostContact(ContactDTO contact);
        Task<ServiceResponse> PutContact(ContactDTO contact);
        Task<bool> ValidateAsync(ContactDTO contact);
    }
}
