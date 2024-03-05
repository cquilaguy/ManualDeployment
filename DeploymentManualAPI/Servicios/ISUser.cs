using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    /// //En la presente Interfaz se definen los metodos a implementar en la clase SUser
    /// </summary>

    public interface ISUser
    {
        
        Task<IEnumerable<User>> GetUsers();
        Task<bool> UserExists(int id);
    }
}
