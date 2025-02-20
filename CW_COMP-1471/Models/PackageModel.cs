using System.ComponentModel.DataAnnotations;

namespace CW_COMP_1471.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}
