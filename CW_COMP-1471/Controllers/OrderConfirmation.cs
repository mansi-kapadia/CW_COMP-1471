using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    [Route("orderconfirmation")]
    [ApiController]
    public class OrderConfirmationController : Controller
    {
        // Display order confirmation page
        [HttpGet("{paymentReferenceNumber}")]
        public IActionResult Index(string paymentReferenceNumber)
        {
            ViewBag.PaymentReferenceNumber = paymentReferenceNumber;
            return View();
        }
    }
}