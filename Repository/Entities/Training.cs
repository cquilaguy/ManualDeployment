using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Training
    {
        [Key]
        [Required]
        public int TrainingID { get; set; }
        [Required]
        [StringLength(5000)]
        public string Comments { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataTraining { get; set; }
        
        [Required]
        [StringLength(5000)]
        public string Objective { get; set; }
        [Required]
        [StringLength(5000)]
        public string Issues { get; set; }

        public int ChangeID { get; set; }
        public int UserID { get; set; }
        public int TypeID { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("TypeID")]
        public virtual Type Type { get; set; }

        // Propiedad de navegación hacia InformationChange
        public Change Changes { get; set; }
    }
}
