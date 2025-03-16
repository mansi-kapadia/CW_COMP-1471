using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    [Route("orderconfirmation")]
    [ApiController]
    public class OrderConfirmationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}