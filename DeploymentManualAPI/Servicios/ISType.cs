using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public interface ISType
    {
        Task<IEnumerable<Repository.Entities.Type>> GetTypes();
        Task<bool> TypeExists(int id);
    }
}
