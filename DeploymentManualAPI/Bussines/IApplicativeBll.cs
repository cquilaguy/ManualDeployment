using DeploymentManualAPI.Response.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por ApplicativeBll donde se da la respuesta JSON 
    /// </summary>

    public interface IApplicativeBll
    {
        Task<ServiceResponse> GetApplicatives();
    }
}
