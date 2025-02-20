using System.ComponentModel.DataAnnotations;

namespace CW_COMP_1471.Models
{
    public class Discount
    {

        [Key]
        public int DiscountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, 99999.99)]
        public decimal DiscountAmount { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public int PackageId { get; set; }

        public Package? Package { get; set; }

    }
}
