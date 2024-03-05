using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Profile

    {
        [key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileID  { get; set; }
        [Required]
        [StringLength(5000)]
        public string ProfileName { get; set; }
      


    }
}
