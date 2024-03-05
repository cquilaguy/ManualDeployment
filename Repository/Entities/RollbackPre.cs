using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class RollbackPre
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int RollbackPreID { get; set; }

        [Required]
        public int Sequence { get; set; }

        [Required]
        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        public int PrerequisiteID { get; set; }

        [ForeignKey("PrerrequisitoID")]
        public virtual Prerequisite Prerequisite { get; set; }
    }
}
