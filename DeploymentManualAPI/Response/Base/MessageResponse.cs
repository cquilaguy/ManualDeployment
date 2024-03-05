using DeploymentManualAPI.Response.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Response.Base
{
    public class MessageResponse
    {
        readonly ServiceResponse response = new ServiceResponse();
        public ServiceResponse MessagesProvider(bool error, object body, int code, string message)
        {
            if (error)
            {
                Status status = new Status
                {
                    StatusCode = code,
                    Severity = "La información ingresada no es válida",
                    StatusDesc = null,
                    AdditionalStatus = new AdditionalStatus
                    {
                        ServerStatusCode = code,
                        Category = "Error Técnico",
                        StatusDesc = message
                    }
                };

                response.MsgRsHdr = new MsgRsHdr
                {
                    Error = new Error
                    {
                        Status = new List<Status>
                        {
                            status
                        }
                    }
                };
            }
            else
            {
                response.MsgRsHdr = new MsgRsHdr
                {
                    Error = null
                };
                response.Body = new Body
                {
                    Result = body
                };
            }
            return response;
        }
    }
}

