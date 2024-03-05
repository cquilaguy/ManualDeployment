using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SPrerrequisito que implementa la lógica de negocio 
    /// </summary>


    public interface ISPrerequisite
    {
        Task<IEnumerable<PrerequisiteDTO>> GetPrerequisite(int ChangeID);
        Task<bool> PrerequisiteExists(int id);
        Task<bool> CheckSequence(int ChangeID, int Sequence);
        Task<Prerequisite> PostPrerequisite(PrerequisiteDTO prerequisite);
        Task<Prerequisite> PutPrerequisite(PrerequisiteDTO prerequisite);
    }
}
