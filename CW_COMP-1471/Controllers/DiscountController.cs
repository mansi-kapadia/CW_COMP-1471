using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using CW_COMP_1471.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : Controller
    {
        private static IDiscountService discountService;
        private readonly ICartService cartService;
        public DiscountController(IDiscountService _discountService, ICartService _cartService)
        {
            // dependency injection
            discountService = _discountService;
            cartService = _cartService;
        }

        // show all discount
        [HttpGet]
        public IActionResult Index()
        {
            return View(discountService.GetAllDiscounts());
        }

        // Create new Discount Page
        [HttpGet("create")]
        public ActionResult CreateDiscount()
        {
            return View();
        }

        // Post discount Details
        [HttpPost("Create")]
        public ActionResult Create([FromForm] Discount discount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View("CreateDiscount", discount);
            }

            discountService.Add(discount);
            return RedirectToAction("Index");
        }

        // Edit Discount Page
        [HttpGet("edit")]
        public ActionResult EditDiscount(int id)
        {
            var discount = discountService.GetById(id);
            if (discount == null)
                return NotFound();

            return View(discount);
        }

        // Post updated Discount
        [HttpPost("Edit")]
        public ActionResult Edit([FromForm] Discount discount)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View("EditDiscount", discount);
            }

            discountService.Update(discount);
            return RedirectToAction("Index");
        }

        // delete discount
        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            discountService.DeleteDiscount(id);
            return RedirectToAction("Index");
        }

        // Validate Discount based on Age and Apply 
        [HttpPost("applydiscount")]
        public async Task<IActionResult> ApplyDiscount(ApplyDiscountRequest request)
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
                return Ok(new { success = true, discountAmount = 0, code = "", ErrorMessage = ex.Message });
            }
        }
    }
}
