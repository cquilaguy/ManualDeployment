using DeploymentManualAPI.Response.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    /// <summary>
    /// Esta clase contiene los metodos que seran implementados por UserBll donde se da la respuesta JSON
    /// </summary>
    public interface IUserBll
    {
        Task<ServiceResponse> GetUsers();

    }
}
