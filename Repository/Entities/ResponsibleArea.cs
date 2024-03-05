using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Repository.Entities
{
    public class ResponsibleArea
    {
        [key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ResponsibleAreaID { get; set; }

        [Required]
        [StringLength(5000)]
        public string ResponsibleName { get; set; }

        [Required]
        [StringLength(5000)]
        public string AreaName { get; set; }
    }
}
