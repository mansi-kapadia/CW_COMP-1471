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
        var booking = await _context.Bookings
            .Where(b => b.UserId == userId && !b.FinalCheckout)
            .Select(b => new CartBooking
            {
                BookingId = b.BookingId,  
                PackageId = b.PackageId,
                DiscountId = b.DiscountId,
            }).FirstOrDefaultAsync();

        var tickets = await _context.Tickets.Where(t => t.BookingId == booking.BookingId).ToListAsync();

        booking.Tickets = await _context.Tickets
                    .Include(t => t.Play) 
                    .Include(t => t.Pricing)
                    .Where(t => t.BookingId == booking.BookingId)
                    .Select(t => new TicketModel
                    {
                        TicketId = t.TicketId,
                        PlayName = t.Play != null ? t.Play.Title : "",
                        SeatNumber = t.SeatNumber,
                        Age = t.Age,
                        PricingType = t.Pricing != null ? t.Pricing.Band : "",
                        PricingId = t.PricingId,
                        Price = t.Pricing != null ? t.Pricing.Price : 0,
                    }).ToListAsync();


        var pricingTypes = await _context.Pricings.ToListAsync();


        var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.DiscountId == booking.DiscountId);
        var package = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == booking.PackageId);

        return new CartModel
        {
            BookingId = booking.BookingId,
            Tickets = booking?.Tickets ?? new List<TicketModel>(),
            PricingTypes = pricingTypes,
            Discount = discount,
            Package = package
        };
    }

    public async Task<Discount> ApplyDiscount(int bookingId, string code)
    {
        var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Code == code);

        if (discount == null)
            return null;

        var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == bookingId);

        if(booking != null)
        {
            booking.DiscountId = discount.DiscountId;
            _context.SaveChangesAsync();
        }

        return discount;
    }

    public async Task<bool> UpdateTicketsAsync(List<TicketUpdateRequest> tickets)
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
                ticket.PricingId = updatedTicket.PricingId;
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

}
