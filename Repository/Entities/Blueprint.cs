using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Blueprint
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BlueprintID { get; set; }
        [Required]
        public float Version { get; set; }  

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int ApplicativeID { get; set; }

        [ForeignKey("ApplicativeID")]
        public virtual Applicative Applicative { get; set; }


    }
}
