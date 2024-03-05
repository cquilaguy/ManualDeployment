using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    /// Esta interfaz contiene los metodos que seran implementados por RollbackPreBll donde se da la respuesta JSON 
    /// </summary>
    public interface IRollbackPreBll
    {
        Task<ServiceResponse> GetRollbackPre(int PrerequisiteID);
        Task<ServiceResponse> PostRollbackPre(RollbackPreDTO rollbackPre);
        Task<ServiceResponse> PutRollbackPre(RollbackPreDTO rollbackPre);
        Task<bool> ValidateAsync(RollbackPreDTO rollbackPre);
    }
}
