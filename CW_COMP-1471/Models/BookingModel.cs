using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("bookings")]
    public class Booking
    {
        [Key]
        [Column("bookingid")]
        public int BookingId { get; set; }

        [Required]
        [Column("userid")]
        public int UserId { get; set; }

        [Column("packageid")]
        public int? PackageId { get; set; }

        [Required]
        [Column("bookingdate")]
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("amount", TypeName = "numeric(10,2)")]
        public decimal Amount { get; set; }

        [Column("discountid")]
        public int? DiscountId { get; set; }

        [Required]
        [Column("paymentstatus")]
        [StringLength(20)]
        public string PaymentStatus { get; set; }

        [Required]
        [Column("finalcheckout")]
        public bool FinalCheckout { get; set; }

       
        // Navigation properties

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PackageId")]
        public virtual Package? Package { get; set; }

        [ForeignKey("DiscountId")]
        public virtual Discount? Discount { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }

    public class AddBookingModel
    {
        public int UserId { get; set; }        
        public int Quantity { get; set; }
        public int PlayId { get; set; }
        public decimal Price { get; set; }
    }

    public class BulkTicketUpdateRequest
    {
        public List<TicketUpdateRequest> Tickets { get; set; }
    }

    public class TicketUpdateRequest
    {
        public int TicketId { get; set; }
        public int Age { get; set; }
        public int PricingId { get; set; }
    }

}
