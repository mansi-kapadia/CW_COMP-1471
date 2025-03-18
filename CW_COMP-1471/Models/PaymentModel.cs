using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("payments")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("paymentid")]
        public int PaymentId { get; set; }

        [Required]
        [Column("bookingid")]
        public int BookingId { get; set; }

        [Required]
        [Column("amount", TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column("discount", TypeName = "decimal(10,2)")]
        public decimal Discount { get; set; } = 0.00m;

        [Column("final_amount", TypeName = "decimal(10,2)")]
        public decimal FinalAmount => Amount - Discount;

        [Required]
        [StringLength(16)]
        [Column("card_number")]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(5)]
        [Column("expiry_date")]
        public string ExpiryDate { get; set; }

        [Required]
        [StringLength(3)]
        [Column("cvv")]
        public string CVV { get; set; }

        [Column("payment_date")]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow; // Default to current timestamp

        [Column("paymentreferencenumber")]
        public Guid? PaymentReferenceNumber { get; set; }

        // Navigation property for Booking
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}





