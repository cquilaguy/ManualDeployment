using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Models;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase STraining que implementa la lógica de negocio
    /// </summary>

    public interface ISTraining
    {
        Task<IEnumerable<TrainingDTO>> GetTraining(int ChangeID);
        Task<bool> TrainingExists(int ChangeID);
        Task<Training> PostTraining(TrainingDTO training);
        Task<Training> PutTraining(TrainingDTO training);
    }
}
