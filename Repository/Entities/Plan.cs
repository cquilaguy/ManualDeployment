using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
  public  class Plan
    {
        [key]
        [Required]
        public int PlanID { get; set; }

        [Required]
        public int Sequence { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataStartTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEndTime { get; set; }

        [Required]
        public int ExecutionTime { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        public int ResponsibleAreaID { get; set; }


        public int ChangeID { get; set; }


        [ForeignKey("ResponsibleAreaID")]
        public virtual ResponsibleArea ResponsibleArea { get; set; }

        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }


        // Propiedad de navegación hacia InformationChange
        [InverseProperty("Plans")]
        public Change Changes { get; set; }
    }
}
