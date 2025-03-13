using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
