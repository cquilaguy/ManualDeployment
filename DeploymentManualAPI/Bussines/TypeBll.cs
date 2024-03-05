using DeploymentManualAPI.Response.Base;
using DeploymentManualAPI.Response.Models;
using DeploymentManualAPI.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Bussines
{
    public class TypeBll:ITypeBll
    {
        private readonly ISType _Repository;
        private readonly MessageResponse _MsgProvider;
        public TypeBll(ISType repository, MessageResponse msgProvider)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
        }
        public async Task<ServiceResponse> GetTypes()
        {
            try
            {
                IEnumerable<Repository.Entities.Type> data = await _Repository.GetTypes();
                if (!data.Any())
                    return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.NotFound, "No se encontraron registros");

                return _MsgProvider.MessagesProvider(false, data, (int)HttpStatusCode.OK, string.Empty);

            }
            catch (Exception ex)
            {
                return _MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
