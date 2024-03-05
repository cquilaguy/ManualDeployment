using DeploymentManualAPI.Response.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public interface ITypeBll
    {
        Task<ServiceResponse> GetTypes();
    }
}
