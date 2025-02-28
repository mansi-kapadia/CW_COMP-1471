using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("packages")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("packageid")]
        public int PackageId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [Column("pricingid")]
        public int PricingId { get; set; }
    }
}
