using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Response.Models
{
    public class MsgRsHdr
    {
        public Error Error { get; set; }
    }

    public class Error
    {
        public List<Status> Status { get; set; }
    }

    public class Status
    {
        public int StatusCode { get; set; }
        public string Severity { get; set; }
        public string StatusDesc { get; set; }
        public AdditionalStatus AdditionalStatus { get; set; }
    }

    public class AdditionalStatus
    {
        public int ServerStatusCode { get; set; }
        public string Category { get; set; }
        public string StatusDesc { get; set; }
    }
}
