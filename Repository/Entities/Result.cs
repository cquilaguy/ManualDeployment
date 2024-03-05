using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
   public class Result
    {
        [key]
        [Required]
        public int ResultID{ get; set; }
        [Required]
        [StringLength(5000)]
        public string Approved { get; set; }
        [Required]
        [StringLength(5000)]
        public string Reprobate { get; set; }
        [Required]
        [StringLength(5000)]
        public string Error { get; set; }
        public int ChangeID { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }
        // Propiedad de navegación hacia InformationChange
        [InverseProperty("Results")]
        public Change Changes { get; set; }
    }
}
