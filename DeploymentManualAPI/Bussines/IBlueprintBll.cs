using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    /// Esta interfaz contiene los metodos que seran implementados por Blueprint donde se da la respuesta JSON 
    /// </summary>
    public interface IBlueprintBll
    {
        Task<ServiceResponse> GetBlueprint(int ApplicativeID);
        Task<ServiceResponse> PostBlueprint(BlueprintDTO blueprint);
        Task<ServiceResponse> PutBlueprint(BlueprintDTO blueprint);
        Task<bool> ValidateAsync(BlueprintDTO blueprint);
    }
}
