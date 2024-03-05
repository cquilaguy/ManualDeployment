using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por PrerequisiteBll donde se da la respuesta JSON 
    /// </summary>

    public interface IPrerequisiteBll
    {
        Task<ServiceResponse> GetPrerequisite(int ChangeID);
        Task<ServiceResponse> PostPrerequisite(PrerequisiteDTO prerequisite);
        Task<ServiceResponse> PutPrerequisite(PrerequisiteDTO prerequisite);
        Task<bool> ValidateAsync(PrerequisiteDTO prerequisite);
    }
}
