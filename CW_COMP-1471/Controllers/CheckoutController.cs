using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.AspNetCore.Mvc;

//namespace CW_COMP_1471.Controllers
//{

[ApiController]
[Route("api/Checkout")]
public class CheckoutController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly ICartService _cartService;
    public CheckoutController(IBookingService bookingService, ICartService cartService)
    {
        _bookingService = bookingService;
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {

        int? userIdNullable = HttpContext.Session.GetInt32("LoggedInUser");
        if (!userIdNullable.HasValue)
        {
            return Unauthorized(new { success = false, message = "User is not logged in." });
        }
        int userId = userIdNullable.Value;

        var booking = await _bookingService.GetBookingByUserId(userId);

        if (booking == null)
        {
            return NotFound("Booking not found.");
        }

        return View(booking);
    }

    [HttpPost("ProcessPayment")]
    public async Task<IActionResult> ProcessPayment([FromBody] CheckoutCart checkoutData)
    {
        if (checkoutData == null || string.IsNullOrEmpty(checkoutData.CreditCardNumber))
        {
            return BadRequest(new { success = false, message = "Invalid payment details." });
        }

        var payment = await _cartService.CheckoutCart(checkoutData);

        if (payment == null)
        {
            return StatusCode(500, new { success = false, message = "Payment processing failed." });
        }

        return Ok(new
        {
            success = true,
            message = "Payment successful!",
            paymentId = payment.PaymentId,
            redirectUrl = $"/OrderConfirmation?PaymentReferenceNumber={payment.PaymentReferenceNumber}"
        });
    }

}
//}
