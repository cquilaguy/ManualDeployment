using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por FunctionalUserBll donde se da la respuesta JSON 
    /// </summary>


    public interface IFunctionalUserBll
    {
        Task<ServiceResponse> GetFunctionalUser(int ChangeID);
        Task<ServiceResponse> PostFunctionalUser(FunctionalUserDTO plan);
        Task<ServiceResponse> PutFunctionalUser(FunctionalUserDTO plan);
        Task<bool> ValidateAsync(FunctionalUserDTO plan);
    }
}
