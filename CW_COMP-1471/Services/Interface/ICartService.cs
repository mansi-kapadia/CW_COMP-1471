using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services.Interface
{
    public interface ICartService
    {
        Task<CartModel> GetCart(int userId);

        Task<Discount> ApplyDiscount(int bookingId, string code);

        Task<bool> UpdateTicketsAsync(List<TicketUpdateRequest> tickets);
    }
}
