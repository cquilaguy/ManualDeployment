using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class ContactDTO
    {
        public int ContactID { get; set; }
        [Required]
        [StringLength(5000)]
        public string Observations { get; set; }

        public int UserID { get; set; }
        public int ChangeID { get; set; }
    }
}
