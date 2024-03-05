using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class RollbackPlan
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int RollbackPlanID { get; set; }

        [Required]
        public int Sequence { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }


        [Required]
        public int PlanID { get; set; }

        [ForeignKey("PlanID")]
        public virtual Plan Plan { get; set; }


    }
}
