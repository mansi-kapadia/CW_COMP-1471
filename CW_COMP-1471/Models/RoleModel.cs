using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("roles")]
    public class Role
    {
        [Key] 
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("rolename")]
        [StringLength(50)]
        public string RoleName { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
