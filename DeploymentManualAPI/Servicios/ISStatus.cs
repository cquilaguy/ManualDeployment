using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public interface ISStatus
    {
        Task<bool> StatusExists(int id);
    }
}
