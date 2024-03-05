using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase STypology que implementa la lógica  
    /// </summary>

    public interface ISTypology
    {
        Task<IEnumerable<Typology>> GetTypologies();
        Task<bool> TypologyExists(int id);
    }
}
