using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{
    public class PricingController : Controller
    {
        public IActionResult Index()
        {
            return View(PricingService.GetPricings());
        }
    }
}
