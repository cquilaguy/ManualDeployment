using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SServer que implementa la lógica de negocio 
    /// </summary>

    public interface ISServer
    {
        Task<IEnumerable<ServerDTO>> GetServer(int EnvironmentID);
        Task<bool> ServerExists(int id);
        Task<Server> PostServer(ServerDTO server);
        Task<Server> PutServer(ServerDTO server);
    }
}
