using CW_COMP_1471.Models;
using CW_COMP_1471.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CW_COMP_1471.Controllers
{
    public class HomeController : Controller
    {
        private static IPlayService playService;
        public HomeController(IPlayService _playService, ILogger<HomeController> logger)
        {
            playService = _playService;
            _logger = logger;
        }

        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            var plays = playService.GetAllPlays(); // Fetches list of plays
            return View(plays);
        }

        //public IActionResult AddToCart(int PlayId)
        //{
        //    var play = PlayService.GetById(PlayId);
        //    if (play == null)
        //        return NotFound();

        //    var ticket = new Ticket { PlayId = PlayId, Play = play };
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