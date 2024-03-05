using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class FunctionalUserDTO
    {
        public int FunctionalUserID { get; set; }


        [Required]
        public int Sequence { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataStartTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataEndTime { get; set; }
        public int ChangeID { get; set; }

        public int UserID { get; set; }
    }
}
