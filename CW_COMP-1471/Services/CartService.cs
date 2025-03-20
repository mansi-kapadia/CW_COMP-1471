using System.Collections.Generic;
using System.Linq;
using CW_COMP_1471;
using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.EntityFrameworkCore;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartModel> GetCart(int userId)
    {
        var newCart = new CartModel();
        var booking = await _context.Bookings
            .Where(b => b.UserId == userId && !b.FinalCheckout)
            .Select(b => new CartBooking
            {
                BookingId = b.BookingId,
                PackageId = b.PackageId,
                DiscountId = b.DiscountId,
            }).FirstOrDefaultAsync();

        if (booking != null)
        {
            booking.Tickets = await _context.Tickets
                        .Include(t => t.Play)
                        //.Include(t => t.Pricing)
                        .Where(t => t.BookingId == booking.BookingId)
                        .Select(t => new TicketModel
                        {
                            TicketId = t.TicketId,
                            PlayName = t.Play != null ? t.Play.Title : "",
                            SeatNumber = t.SeatNumber,
                            Age = t.Age,
                            Amount = t.Amount,
                            PlayId = t.PlayId
                        }).ToListAsync();

            //var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.DiscountId == booking.DiscountId);
            var package = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == booking.PackageId);

            newCart.BookingId = booking.BookingId;
            newCart.Tickets = booking.Tickets;
            //newCart.Discount = discount;
            newCart.Package = package;
        }

        var pricingTypes = await _context.Pricings.ToListAsync();

        newCart.PricingTypes = pricingTypes;

        return newCart;
    }

    public async Task<Discount> ApplyDiscount(int bookingId, string code, List<int> Ages)
    {
        var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Code == code);

        if (discount == null)
            return null;

        if (discount.IsForKids && Ages.Count(x => x < 12) == 0)
            throw new Exception("Discount is only valid for Kids below age 12.");

        if (discount.IsForPensioners && Ages.Count(x => x > 60) == 0)
            throw new Exception("Discount is only valid for Pensioners above age 60.");

        if (discount.ExpiryDate < DateTime.Now)
            throw new Exception("Discount Code is Expired!");

        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == bookingId);

        if (booking != null)
        {
            booking.DiscountId = discount.DiscountId;
            _context.SaveChangesAsync();
        }

        return discount;
    }

    public async Task<bool> UpdateBookingAsync(List<TicketUpdateRequest> tickets)
    {
        if (tickets == null || tickets.Count == 0)
            return false;

        var ticketIds = tickets.Select(t => t.TicketId).ToList();
        var dbTickets = await _context.Tickets.Where(t => ticketIds.Contains(t.TicketId)).ToListAsync();

        foreach (var ticket in dbTickets)
        {
            var updatedTicket = tickets.FirstOrDefault(t => t.TicketId == ticket.TicketId);
            if (updatedTicket != null)
            {
                ticket.Age = updatedTicket.Age;
            }
        }

        // update booking Status
        Booking booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == dbTickets.FirstOrDefault().BookingId);

        booking.Amount = dbTickets.Sum(x => x.Amount)
               - (booking.Discount?.DiscountAmount ?? 0.0m);
        booking.PaymentStatus = "In Prog";
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<Payment> CheckoutCart(CheckoutCart checkout)
    {
        // Get the booking based on BookingId
        Booking booking = await _context.Bookings
                                .FirstOrDefaultAsync(b => b.BookingId == checkout.BookingId);

        if (booking == null)
            throw new Exception("Booking not found.");

        booking.FinalCheckout = true;
        booking.PaymentStatus = "Paid";

        decimal discount = booking.Discount?.DiscountAmount ?? 0.0m;

        // Create a new Payment entry
        Payment payment = new Payment
        {
            BookingId = checkout.BookingId,
            Amount = booking.Amount,
            Discount = discount,
            PaymentReferenceNumber = Guid.NewGuid(),
            CardNumber = checkout.CreditCardNumber,
            ExpiryDate = checkout.ExpiryDate,
            CVV = checkout.CVV,
            PaymentDate = DateTime.UtcNow
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }
}
