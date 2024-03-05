using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Entities;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SEnvironment que implementa la lógica de negocio 
    /// </summary>

    public interface ISEnvironment
    {
        Task<IEnumerable<Repository.Entities.Environment>> GetEnvironments();
        Task<bool> EnvironmentExists(int id);
    }
}
