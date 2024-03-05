using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SRollbackPre que implementa la lógica de negocio 
    /// </summary>

    public interface ISRollbackPre
    {
        Task<IEnumerable<RollbackPreDTO>> GetRollbackPre(int PrerequisiteID);
        Task<bool> RollbackPreExists(int id);
        Task<RollbackPre> PostRollbackPre(RollbackPreDTO rollbackpre);
        Task<RollbackPre> PutRollbackPre(RollbackPreDTO rollbackpre);
    }
}
