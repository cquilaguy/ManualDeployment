using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Prerequisite
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PrerequisiteID { get; set; }

        [Required]
        public int Sequence { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataStart { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEnd { get; set; }
        [Required]
        public int ExecutionTime { get; set; }
        public int ResponsibleAreaID { get; set; }
        public int ChangeID { get; set; }
        public int UserID { get; set; }

        [ForeignKey("ResponsibleAreaID")]
        public virtual ResponsibleArea ResponsibleArea { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }



        // Propiedad de navegación hacia InformationChange
        [InverseProperty("Prerequisites")]
        public Change Changes { get; set; }
    }
}
