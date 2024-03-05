using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class PrerequisiteDTO
    {
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
    }
}
