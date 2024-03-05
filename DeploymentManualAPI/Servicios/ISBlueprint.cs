using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SBlueprint que implementa la lógica de negocio 
    /// </summary>
    public interface ISBlueprint
    {
        Task<IEnumerable<BlueprintDTO>> GetBlueprint(int ApplicativeID);
        Task<bool> BlueprintExists(int id);
        Task<Blueprint> PostBlueprint(BlueprintDTO blueprint);
        Task<Blueprint> PutBlueprint(BlueprintDTO blueprint);
    }
}
