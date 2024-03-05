using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por PostImplantationBll donde se da la respuesta JSON
    /// </summary>


    public interface IPostImplantationBll
    {
        Task<ServiceResponse> GetPostImplantation(int ChangeID);
        Task<ServiceResponse> PostPostImplantation(PostImplantationDTO postImplantation);
        Task<ServiceResponse> PutPostImplantation(PostImplantationDTO postImplantation);
        Task<bool> ValidateAsync(PostImplantationDTO postImplantation);
    }
}
