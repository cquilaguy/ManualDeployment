using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Server
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ServerID { get; set; }

        [Required]
        [StringLength(5000)]
        public string NameServer { get; set; }

        public int EnvironmentID { get; set; }

        [ForeignKey("EnvironmentID")]
        public virtual Environment Environment{ get; set; }
    }
}
