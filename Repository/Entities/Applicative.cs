using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace Repository.Entities
{
    public class Applicative
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ApplicativeID { get; set; }

        [Required]
        [StringLength(5000)]
        public string NameApplicative { get; set; }

       
        
    }
}
    
   
