using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    /// Esta interfaz contiene los metodos que seran implementados por RollbackPlanBll donde se da la respuesta JSON 
    /// </summary>
    public interface IRollbackPlanBll
    {
        Task<ServiceResponse> GetRollbackPlan(int PlanID);
        Task<ServiceResponse> PostRollbackPlan(RollbackPlanDTO rollbackPlan);
        Task<ServiceResponse> PutRollbackPlan(RollbackPlanDTO rollbackPlan);
        Task<bool> ValidateAsync(RollbackPlanDTO rollbackPlan);
    }
}
