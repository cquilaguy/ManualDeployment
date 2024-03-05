using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SApplicative que implementa la lógica de negocio 
    /// </summary>

    public interface ISApplicative
    {
        Task<IEnumerable<Applicative>> GetApplicatives();
        Task<bool> ApplicativeExists(int id);
    }
}
