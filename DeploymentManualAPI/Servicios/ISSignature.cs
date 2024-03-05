using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SSignature que implementa la lógica de negocio 
    /// </summary>

    public interface ISSignature
    {
        Task<IEnumerable<SignatureDTO>> GetSignature(int ChangeID);
        Task<bool> SignatureExists(int ChangeID);
        Task<Signature> PostSignature(SignatureDTO signature);
        Task<Signature> PutSignature(SignatureDTO signature);
    }
}
