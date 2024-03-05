using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class BlueprintDTO
    {
        public int BlueprintID { get; set; }
        [Required]
        public float Version { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int ApplicativeID { get; set; }
    }
}
