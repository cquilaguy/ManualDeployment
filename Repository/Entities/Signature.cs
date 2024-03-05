using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
   public class Signature
    {
        [Key]
        [Required]
        public int SignatureID { get; set; }
        [Required]
        [StringLength(5000)]
        public string Observatins { get; set; }
        public string EmailName { get; set; }

        public int ChangeID { get; set; }

        public int UserID { get; set; }

        public int TrainedUserID { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }
         
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        //Revisar rainedUser como llave foranea

        // Propiedad de navegación hacia InformationChange
        [InverseProperty("Signatures")]
        public Change Changes { get; set; }
    }
}
