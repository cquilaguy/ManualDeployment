using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class FunctionalUser
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int FunctionalUserID{ get; set; }

        
        [Required]
        public int Sequence{ get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataStartTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEndTime { get; set; }
        public int ChangeID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        // Propiedad de navegación hacia InformationChange
        [InverseProperty("FunctionalUsers")]
        public Change Changes { get; set; }
    }
}
