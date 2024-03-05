using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Entities
{
    public class Type
    {
        [key]
        [Required]
        public int TypeID { get; set; }
        [Required]
        [StringLength(5000)]
        public string TypeName { get; set; }
    }
}
