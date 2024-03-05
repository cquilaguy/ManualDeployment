using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por TrainingBll donde se da la respuesta JSON
    /// </summary>

    public interface ITrainingBll
    {
        Task<ServiceResponse> GetTraining(int ChangeID);
        Task<ServiceResponse> PostTraining(TrainingDTO signature);

        Task<ServiceResponse> PutTraining(TrainingDTO training);

        Task<bool> ValidateAsync(TrainingDTO training);
    }
}
