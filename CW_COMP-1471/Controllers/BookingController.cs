using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CW_COMP_1471.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;
        private readonly ICartService _cartService;

        public BookingController(IBookingService bookingService, ICartService cartService)
        {
            // dependency injection
            _bookingService = bookingService;
            _cartService = cartService;

        }

        // add new booking
        [HttpPost]
        public async Task<IActionResult> AddToBooking([FromBody] AddBookingModel booking)
        {
            try
            {
                booking.UserId = (int)HttpContext.Session.GetInt32("LoggedInUser");

                if (booking == null || booking.Quantity <= 0)
                {
                    return BadRequest(new { message = "Invalid booking details." });
                }

                int TicketCount = await _bookingService.AddToBooking(booking);
                return Ok(new { result = TicketCount, message = TicketCount + " tickets added to Booking successfully!" });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        // update Age
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

        // Display bookings
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userIdNullable = HttpContext.Session.GetInt32("LoggedInUser");
            if (!userIdNullable.HasValue)
            {
                return Unauthorized(new { success = false, message = "User is not logged in." });
            }
            int userId = userIdNullable.Value;

            CartModel model = await _cartService.GetCart(userId);
            return View(model);
        }
    }

}
