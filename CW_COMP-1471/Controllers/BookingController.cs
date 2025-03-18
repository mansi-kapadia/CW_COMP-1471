using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CW_COMP_1471.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ICartService _cartService;

        public BookingController(IBookingService bookingService, ICartService cartService)
        {
            _bookingService = bookingService;
            _cartService = cartService;
        }

        [HttpPost]        
        public async Task<IActionResult> AddToCart([FromBody] AddBookingModel booking)
        {
            try
            {
                booking.UserId = (int)HttpContext.Session.GetInt32("LoggedInUser");

                if (booking == null || booking.Quantity <= 0)
                {
                    return BadRequest(new { message = "Invalid booking details." });
                }

                int TicketCount = await _bookingService.AddToCart(booking);
                return Ok(new { result = TicketCount , message = TicketCount + " tickets added to cart successfully!" });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet("/api/bookings/getcartcount")]
        public async Task<int> GetCartCount()
        {
            try
            {
                int userId = (int)HttpContext.Session.GetInt32("LoggedInUser");

                int TicketCount = await _bookingService.GetCartCount(userId);
                return TicketCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost("update-bookings")]
        public async Task<IActionResult> UpdateTickets([FromBody] BulkTicketUpdateRequest request)
        {
            if (request == null || request.Tickets == null || request.Tickets.Count == 0)
            {
                return BadRequest(new { success = false, message = "No ticket data received." });
            }

            bool isUpdated = await _cartService.UpdateBookingAsync(request.Tickets);

            if (!isUpdated)
            {
                return BadRequest(new { success = false, message = "Ticket update failed." });
            }

            return Ok(new { success = true, message = "Tickets updated successfully!" });
        }
    }

}
