using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW_COMP_1471.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [Key]
        [Column("ticketid")]
        public int TicketId { get; set; }

        [Required]
        [Column("bookingid")]
        public int BookingId { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("userid")]
        public int UserId { get; set; }

        [Required]
        [Column("status")]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        [Column("seatnumber")]
        [StringLength(10)]
        public string SeatNumber { get; set; }

        [Column("age")]
        public int Age { get; set; } = 18;

        [Column("playid")]
        public int PlayId { get; set; }

        // Navigation properties
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }

        //[ForeignKey("PricingId")]
        //public virtual Pricing? Pricing { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PlayId")]
        public virtual Play Play { get; set; }

    }

    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public int PricingId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string SeatNumber { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
