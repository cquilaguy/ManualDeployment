using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Models
{
    public class ChangeDTO
    {
        public int ChangeID { get; set; }

        [Required]
        [StringLength(5000)]
        public string ChangeDescription { get; set; }

        [Required]
        [StringLength(5000)]
        public string ChangeNumber { get; set; }

        [Required]
        [RegularExpression("^(1|0)$")]
        public int CheckList { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ModificationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DeploymentDate { get; set; }





        [Required]
        [Range(1, 100)]
        public int Version { get; set; }
        [Required]
        [RegularExpression("^(1|0)$")]
        public int IsTemplate { get; set; }

        [Required]
        [StringLength(5000)]
        public string Observations { get; set; }


        [Required]
        public int UserID { get; set; }
        
        [Required]
        public int StatusID { get; set; }

        [Required]
        public int EnvironmentID { get; set; }

        [Required]
        public int RequestTypeID { get; set; }

        [Required]
        public int TypologyID { get; set; }

    }
}
