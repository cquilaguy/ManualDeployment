using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class ProfileUser

    {
        [key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileUserID{ get; set; }

        [Required]
        [RegularExpression("^(0|1)$", ErrorMessage = "El valor debe ser '0' o '1'.")]
        public int State { get; set; }

        public int ProfileID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("ProfileID")]
        public virtual Profile Profile { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
