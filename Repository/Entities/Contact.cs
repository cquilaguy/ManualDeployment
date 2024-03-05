using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ContactID{ get; set; }
        [Required]
        [StringLength(5000)]
        public string Observations { get; set; }

        public int UserID { get; set; }
        public int ChangeID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }


        // Propiedad de navegación hacia InformationChange
        [InverseProperty("Contacts")]
        public Change Changes { get; set; }
    }
}
