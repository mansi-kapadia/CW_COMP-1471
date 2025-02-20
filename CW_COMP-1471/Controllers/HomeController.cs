using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CW_COMP_1471.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //private static List<Ticket> Cart = new List<Ticket>();

        public IActionResult Index()
        {
            var plays = PlayService.GetAllPlays(); // Fetches list of plays
            return View(plays);
        }

        //public IActionResult AddToCart(int playId)
        //{
        //    var play = PlayService.GetById(playId);
        //    if (play == null)
        //        return NotFound();

        //    var ticket = new Ticket { PlayId = playId, Play = play };
        //    return View(ticket);
        //}

        //[HttpPost]
        //public IActionResult AddToCart(Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Cart.Add(ticket);
        //        return RedirectToAction("Cart");
        //    }
        //    return View(ticket);
        //}

        //public IActionResult Cart()
        //{
        //    return View(Cart);
        //}

        //public IActionResult RemoveFromCart(int ticketId)
        //{
        //    var ticket = Cart.Find(t => t.TicketId == ticketId);
        //    if (ticket != null)
        //        Cart.Remove(ticket);

        //    return RedirectToAction("Cart");
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}