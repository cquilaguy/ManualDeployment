using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Response.Models
{
    public class ServiceResponse
    {
        [JsonPropertyName("MsgRsHdr")]
        public MsgRsHdr MsgRsHdr { get; set; }
        public Body Body { get; set; }
    }
}
