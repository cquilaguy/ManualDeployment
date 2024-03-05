using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SChange que implementa la lógica de negocio 
    /// </summary>

    public interface ISChange
    {
        Task<ChangeDTO> GetChange(int id);
        Task<ChangeObj> GetChangeS(string changeNumber);
        Task<bool> ChangeExists(int id);
        Task<Change> PostChange(ChangeDTO change);
        Task<Change> PutChange(ChangeDTO change);
        Task <FilterObj> GetFilteredChanges(FilterDTO filter);

    }
}
