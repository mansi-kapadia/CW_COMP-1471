using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services.Interface
{
    public interface IBookingService
    {
        Task<int> AddToBooking(AddBookingModel bookingModel);
        Task<Booking> GetBookingByUserId(int bookingId);
    }
}
