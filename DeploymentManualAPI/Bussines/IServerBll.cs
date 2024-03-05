using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por UserBll donde se da la respuesta JSON
    /// </summary>

    public interface IServerBll
    {
        Task<ServiceResponse> GetServer(int EnvironmentID);
        Task<ServiceResponse> PostServer(ServerDTO server);
        Task<ServiceResponse> PutServer(ServerDTO server);
        Task<bool> ValidateAsync(ServerDTO server);
    }
}
