using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    /// Esta interfaz representa todos los metodos que tendrá la clase SRequestType
    /// </summary>

    public interface ISRequestType
    {
        Task<IEnumerable<RequestType>> GetRequestTypes();
        Task<bool> RequestTypeExists(int id);
    }
}
