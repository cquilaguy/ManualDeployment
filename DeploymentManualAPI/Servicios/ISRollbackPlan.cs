using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SRollbackPlan que implementa la lógica de negocio 
    /// </summary>
    public interface ISRollbackPlan
    {
        Task<IEnumerable<RollbackPlanDTO>> GetRollbackPlan(int PlanID);
        Task<bool> RollbackPlanExists(int id);
        Task<RollbackPlan> PostRollbackPlan(RollbackPlanDTO rollbackPlan);
        Task<RollbackPlan> PutRollbackPlan(RollbackPlanDTO rollbackPlan);
    }
}
