using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    [Route("orderconfirmation")]
    [ApiController]
    public class OrderConfirmationController : Controller
    {
        // Display order confirmation page
        public IActionResult Index()
        {
            return View();
        }
    }
}