using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class RollbackPreDTO
    {
        public int RollbackPreID { get; set; }

        [Required]
        public int Sequence { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        public int PrerequisiteID { get; set; }
    }
}
