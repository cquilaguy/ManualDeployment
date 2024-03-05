using DeploymentManualAPI.Response.Models;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    ///Esta interfaz contiene los metodos que seran implementados por SignatureBll donde se da la respuesta JSON 
    /// </summary>

    public interface ISignatureBll
    {
        Task<ServiceResponse> GetSignature(int ChangeID);
        Task<ServiceResponse> PostSignature(SignatureDTO signature);

        Task<ServiceResponse> PutSignature(SignatureDTO signature);

        Task<bool> ValidateAsync(SignatureDTO signature);
    }
}
