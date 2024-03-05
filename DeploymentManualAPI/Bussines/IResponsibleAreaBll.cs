using DeploymentManualAPI.Response.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    //Esta interfaz contiene los metodos que seran implementados por ResponsibleAreaBll donde se da la respuesta JSON
    public interface IResponsibleAreaBll
    {
        Task<ServiceResponse> GetResponsibleAreas();
    }
}
