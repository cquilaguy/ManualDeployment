using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Postimplantation

    {
        [key]
        [Required]
        public int PostimplantationID{ get; set; }
        [Required]
        public int Sequence { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description  { get; set; }
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
        [InverseProperty("Postimplantations")]
        public Change Changes { get; set; }
    }
}
