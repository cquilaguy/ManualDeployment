using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SPlan que implementa la lógica de negocio 
    /// </summary>


    public interface ISPlan
    {
        Task<IEnumerable<PlanDTO>> GetPlan(int ChangeID);
        Task<bool> PlanExists(int id);
        Task<bool> CheckSequence(int ChangeID, int Sequence);
        Task<Plan> PostPlan(PlanDTO plan);
        Task<Plan> PutPlan(PlanDTO plan);
    }
}
