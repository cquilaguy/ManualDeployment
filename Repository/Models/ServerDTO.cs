using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class ServerDTO
    {
        public int ServerID { get; set; }

        [Required]
        [StringLength(5000)]
        public string NameServer { get; set; }

        public int EnvironmentID { get; set; }
    }
}
