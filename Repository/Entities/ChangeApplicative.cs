using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class ChangeApplicative
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ChangeApplicativeID { get; set; }

        public int ChangeID { get; set; }

        public int ApplicativeID { get; set; }


        [ForeignKey("ChangeID")]
        public virtual Change Change { get; set; }

        [ForeignKey("ApplicativeID")]
        public virtual Applicative Applicative { get; set; }

        



    }
}
