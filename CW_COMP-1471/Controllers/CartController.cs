using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/cart")]
public class CartController : Controller
{
    private readonly ICartService cartService;

    public CartController(ICartService _cartService)
    {
        cartService = _cartService;

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

        CartModel model = await cartService.GetCart(userId);
        return View(model);
    }

    [HttpPost("applydiscount")]
    public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountRequest request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.DiscountCode) || request.BookingId <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid discount request." });
            }

            var discount = await cartService.ApplyDiscount(request.BookingId, request.DiscountCode, request.Ages);

            if (discount == null)
            {
                return NotFound(new { success = false, message = "Invalid Discount Code." });
            }

            return Ok(new { success = true, discountAmount = discount.DiscountAmount, code = discount.Code });
        }
        catch (Exception ex)
        {
            return Ok(new { success = true, discountAmount = 0, code = "" , ErrorMessage = ex.Message });
        }
    }
    
}
