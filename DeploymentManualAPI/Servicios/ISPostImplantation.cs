using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    /// <summary>
    ///Esta interfaz representa todos los metodos que tendrá la clase SPostImplantationH que implementa la lógica de negocio 
    /// </summary>


    public interface ISPostImplantation
    {
        Task<IEnumerable<PostImplantationDTO>> GetPostImplantation(int ChangeID);
        Task<bool> PostImplantationExists(int id);
        Task<bool> CheckSequence(int ChangeID, int Sequence);
        Task<Postimplantation> PostPostImplantation(PostImplantationDTO postImplantation);
        Task<Postimplantation> PutPostImplantation(PostImplantationDTO postImplantation);
    }
}
