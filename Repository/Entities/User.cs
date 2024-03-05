
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID{ get; set; }
        [Required]
        [StringLength(5000)]
        public string NetworkUser { get; set; }
        [Required]
        [StringLength(5000)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public int Phone { get; set; }   
        [Required]
        [StringLength(5000)]
        public string Area { get; set; }
        [Required]
        [StringLength(5000)]
        public string Position { get; set; }


    }
}
