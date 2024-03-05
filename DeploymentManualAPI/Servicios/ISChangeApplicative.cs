using Repository.Entities;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Servicios
{
    public interface ISChangeApplicative
    {
        /// <summary>
        ///Esta interfaz representa todos los metodos que tendrá la clase SChangeApplicative que implementa la lógica de negocio 
        /// </summary>
            Task<IEnumerable<ChangeApplicativeDTO>> GetChangeApplicative(int ChangeID);
            Task<bool> ChangeApplicativeExists(int ChangeID);
            Task<ChangeApplicative> PostChangeApplicative(ChangeApplicativeDTO changeApplicative);
            Task<ChangeApplicative> PutChangeApplicative(ChangeApplicativeDTO changeApplicative);
        }
    }

