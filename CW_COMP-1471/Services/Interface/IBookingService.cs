using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services.Interface
{
    public interface IBookingService
    {
        Task<int> AddToCart(AddBookingModel bookingModel);
        Task<int> GetCartCount(int UserId);
        public Task<List<CartItemModel>> GetCartDetails(int userId);
    }
}
