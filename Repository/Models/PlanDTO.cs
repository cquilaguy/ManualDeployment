using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class PlanDTO
    {
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
    }
}
