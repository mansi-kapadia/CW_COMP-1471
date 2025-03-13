namespace CW_COMP_1471.Models
{
    public class CartModel
    {
        public int BookingId { get; set; }
        public Discount Discount { get; set; }
        public Package Package { get; set; }
        public List<TicketModel> Tickets { get; set; }
        public List<Pricing> PricingTypes { get; set; }
    }

    public class CartBooking
    {
        public int BookingId { get; set; }
        public int? PackageId { get; set; }
        public int? DiscountId { get; set; }
        public List<TicketModel> Tickets { get; set; }
    }

    public class TicketModel
    {
        public int TicketId { get; set; }
        public int? PricingId { get; set; }
        public string PlayName { get; set; }
        public string SeatNumber { get; set; }
        public int Age { get; set; }
        public string PricingType { get; set; }
        public double Price { get; set; }
    }

    public class UpdateTicketRequest
    {
        public int TicketId { get; set; }
        public int Age { get; set; }
        public int PricingId { get; set; }
    }

    public class ApplyDiscountRequest
    {
        public int BookingId { get; set; }
        public string DiscountCode { get; set; }
    }

}
