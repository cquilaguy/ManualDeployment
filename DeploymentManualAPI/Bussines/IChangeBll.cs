using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por ChangeBll donde se da la respuesta JSON
    /// </summary>
   
    public interface IChangeBll
    {
        Task<ServiceResponse> GetChange(int id);
        Task<ServiceResponse> GetChangeS(string changeNumber);

        Task<ServiceResponse> PostChange(ChangeDTO change);

        Task<ServiceResponse> PutChange(ChangeDTO change);

        Task<bool> ValidateAsync(ChangeDTO change);

        Task<ServiceResponse> GetFilteredChanges(FilterDTO filter);

    }
}
