using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por PlanBll donde se da la respuesta JSON 
    /// </summary>

    public interface IPlanBll
    {
        Task<ServiceResponse> GetPlan(int ChangeID);
        Task<ServiceResponse> PostPlan(PlanDTO plan);
        Task<ServiceResponse> PutPlan(PlanDTO plan);
        Task<bool> ValidateAsync(PlanDTO plan);
    }
}
