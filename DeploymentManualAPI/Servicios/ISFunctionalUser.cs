using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SFunctionalUser que implementa la lógica de negocio
    /// </summary>
    public interface ISFunctionalUser
    {
        Task<IEnumerable<FunctionalUserDTO>> GetFunctionalUser(int ChangeID);
        Task<bool> FunctionalUserExists(int id);
        Task<bool> CheckSequence(int ChangeID, int Sequence);
        Task<FunctionalUser> PostFunctionalUser(FunctionalUserDTO functionalUser);
        Task<FunctionalUser> PutFunctionalUser(FunctionalUserDTO functionalUser);
    }
}
