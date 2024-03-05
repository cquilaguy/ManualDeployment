using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Change
    {
       

        [Key]
        [Required]
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
        public DateTime  ApplicationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DeploymentDate { get; set; }

        [Required]
        [Range(1, 100)]
        public int Version{ get; set; }
        [Required]
        [RegularExpression("^(1|0)$")]
        public int IsTemplate { get; set; }

        [Required]
        [StringLength(5000)]
        public string Observations{ get; set; }


        [Required]
        public int UserID{ get; set; }

        [Required]
        public int StatusID { get; set; }

        [Required]
        public int EnvironmentID { get; set; }

        [Required]
        public int RequestTypeID { get; set; }

        [Required]
        public int TypologyID { get; set; }



        [ForeignKey("UserID")]
        public virtual User User{ get; set; }

        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }

        [ForeignKey("EnvironmentID")]
        public virtual Environment Environment { get; set; }

        [ForeignKey("RequestTypeID")]
        public virtual RequestType RequestType { get; set; }

        [ForeignKey("TypologyID")]
        public virtual Typology Typology { get; set; }




        // Propiedad de navegación hacia AcceptanceSignatures
        [InverseProperty("Changes")]
        public ICollection<Signature> Signatures { get; set; }
        // Propiedad de navegación hacia AcceptanceSignatures
        [InverseProperty("Changes")]
        public ICollection<Training> Trainings { get; set; }

        [InverseProperty("Changes")]
        public ICollection<Contact> Contacts { get; set; }

        [InverseProperty("Changes")]
        public ICollection<Prerequisite> Prerequisites { get; set; }
        [InverseProperty("Changes")]
        public ICollection<Result> Results { get; set; }
        [InverseProperty("Changes")]
        public ICollection<Plan> Plans { get; set; }
        [InverseProperty("Changes")]
        public ICollection<Postimplantation> Postimplantations { get; set; }
        [InverseProperty("Changes")]
        public ICollection<FunctionalUser> FunctionalUsers { get; set; }

    }
}
