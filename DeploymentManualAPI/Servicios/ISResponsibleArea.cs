using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///  //En esta clase se crean los metodos que heredará el servicio SResponsibleArea
    /// </summary>
    public interface ISResponsibleArea
    {   
        Task<IEnumerable<ResponsibleArea>> GetResponsibleAreas();
        Task<bool> ResponsibleAreaExists(int id);
    }
}
