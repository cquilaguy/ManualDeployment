using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por ChangeApplicativeBll donde se da la respuesta JSON 
    /// </summary>
    public interface IChangeApplicativeBll
    {
        Task<ServiceResponse> GetChangeApplicative(int ChangeID);
        Task<ServiceResponse> PostChangeApplicative(ChangeApplicativeDTO changeApplicative);
        Task<ServiceResponse> PutChangeApplicative(ChangeApplicativeDTO changeApplicative);
        Task<bool> ValidateAsync(ChangeApplicativeDTO changeApplicative);
    }
}
