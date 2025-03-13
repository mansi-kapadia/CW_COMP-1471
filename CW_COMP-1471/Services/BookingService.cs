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

            _context.Tickets.AddRange(tickets);
            await _context.SaveChangesAsync();

            return existingTicketCount + bookingModel.Quantity;
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

        public async Task<List<CartItemModel>> GetCartDetails(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId && !b.FinalCheckout)
                .Include(b => b.Tickets)
                .ToListAsync();

            var cartItems = bookings.SelectMany(booking => booking.Tickets.Select(ticket => new CartItemModel
            {
                TicketId = ticket.TicketId,
                PlayName = ticket.Play.Title,
                SeatNumber = ticket.SeatNumber,
                Age = ticket.Age,
                PricingType = ticket.Pricing.Band
            })).ToList();

            return cartItems;
        }

    }
}
