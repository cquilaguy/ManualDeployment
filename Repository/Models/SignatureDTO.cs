using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class SignatureDTO
    {
        public int SignatureID { get; set; }
        [Required]
        [StringLength(5000)]
        public string Observatins { get; set; }
        public string EmailName { get; set; }

        public int ChangeID { get; set; }

        public int UserID { get; set; }

        public int TrainedUserID { get; set; }
    }
}
