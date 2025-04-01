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

        // Add Ticket To Cart
        public async Task<int> AddToBooking(AddBookingModel bookingModel)
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
                    SeatNumber = $"A{existingTicketCount + i + 1}",
                    Amount = bookingModel.Price
                });
            }

            int ticketCount = existingTicketCount + bookingModel.Quantity;

            if (existingBooking.PackageId == null)
            {
                Package package = await _context.Packages.Where(x => x.MinTicketQty == ticketCount).OrderByDescending(x => x.FreeTicketCount).ThenByDescending(x => x.MinTicketQty).FirstOrDefaultAsync();

                if (package != null)
                {
                    PricingBand pricing = await _context.Pricings.FirstOrDefaultAsync(x => x.Price == 0);

                    for (int i = 0; i < package.FreeTicketCount; i++)
                    {
                        tickets.Add(new Ticket
                        {
                            BookingId = existingBooking.BookingId,
                            UserId = bookingModel.UserId,
                            Age = 18,
                            Status = "Selected",
                            PlayId = bookingModel.PlayId,
                            SeatNumber = $"A{existingTicketCount + i + 1}", // Generate Ticket Number 
                            Amount = 0 // price is 0 for free tickets
                        });
                    }

                    existingBooking.PackageId = package.PackageId;
                    await _context.SaveChangesAsync();
                }
            }

            _context.Tickets.AddRange(tickets);
            await _context.SaveChangesAsync();


            return ticketCount;
        }

        // Get Pending Booking details from UserId 
        public async Task<Booking> GetBookingByUserId(int UserId)
        {
            var booking = await _context.Bookings
                            .Include(x => x.Discount)
                            .Include(x => x.Tickets)
                            .AsNoTracking()
               .FirstOrDefaultAsync(b => b.UserId == UserId && b.FinalCheckout == false);

            if (booking != null) booking.Amount = booking.Tickets.Sum(x => x.Amount) - (booking.Discount?.DiscountAmount ?? 0.0m);

            return booking;
        }
    }
}
