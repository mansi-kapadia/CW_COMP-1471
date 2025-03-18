using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CW_COMP_1471.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddToCart(AddBookingModel bookingModel)
        {
            var existingBooking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.UserId == bookingModel.UserId && !b.FinalCheckout);

            if (existingBooking == null)
            {
                existingBooking = new Booking
                {
                    UserId = bookingModel.UserId,
                    BookingDate = DateTime.UtcNow,
                    Amount = 0,
                    PaymentStatus = "Pending",
                    FinalCheckout = false
                };

                _context.Bookings.Add(existingBooking);
                await _context.SaveChangesAsync();
            }

            var tickets = new List<Ticket>();
            int existingTicketCount = await _context.Tickets.CountAsync(t => t.BookingId == existingBooking.BookingId);

            for (int i = 0; i < bookingModel.Quantity; i++)
            {
                tickets.Add(new Ticket
                {
                    BookingId = existingBooking.BookingId,
                    UserId = bookingModel.UserId,
                    Age = 18,
                    Status = "Selected",
                    PlayId = bookingModel.PlayId,
                    SeatNumber = $"A{existingTicketCount + i + 1}"
                });
            }

            int ticketCount = existingTicketCount + bookingModel.Quantity;

            Package package = await _context.Packages.Where(x => x.MinTicketQty <= ticketCount).OrderByDescending(x => x.FreeTicketCount).ThenByDescending(x => x.MinTicketQty).FirstOrDefaultAsync(); 

            if (package != null)
            {
                Pricing pricing = await _context.Pricings.FirstOrDefaultAsync(x => x.Price == 0);

                for (int i = 0; i < package.FreeTicketCount; i++)
                {
                    tickets.Add(new Ticket
                    {
                        BookingId = existingBooking.BookingId,
                        UserId = bookingModel.UserId,
                        Age = 18,
                        Status = "Selected",
                        PlayId = bookingModel.PlayId,
                        SeatNumber = $"A{existingTicketCount + i + 1}",
                        PricingId = pricing.PricingId
                    });
                }
            }

            _context.Tickets.AddRange(tickets);
            await _context.SaveChangesAsync();


            return ticketCount;
        }

        public async Task<Booking> GetBookingByUserId(int UserId)
        {
            var booking = await _context.Bookings
                            .Include(x => x.Discount)
                            .Include(x => x.Tickets).ThenInclude(x => x.Pricing)
                            .AsNoTracking()
               .FirstOrDefaultAsync(b => b.UserId == UserId && b.FinalCheckout == false);

            booking.Amount = booking.Tickets.Sum(x => x.Pricing != null ? (decimal)(x.Pricing.Price) : 0.0m) - (booking.Discount?.DiscountAmount ?? 0.0m);

            return booking;
        }

        public async Task<int> GetCartCount(int UserId)
        {
            var existingBooking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.UserId == UserId && !b.FinalCheckout);

            int existingTicketCount = 0;

            if (existingBooking != null) existingTicketCount = await _context.Tickets
                            .CountAsync(t => t.BookingId == existingBooking.BookingId);

            return existingTicketCount;
        }

    }
}
