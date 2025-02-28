using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("discounts")]
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("discountid")]
        public int DiscountId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("code")]
        public string Code { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [Range(0, 99999.99)]
        [Column("discountamount", TypeName = "numeric(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column("expirydate", TypeName = "date")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Column("packageid")]
        public int PackageId { get; set; }

        [ForeignKey("PackageId")]
        public Package? Package { get; set; }
    }
}
