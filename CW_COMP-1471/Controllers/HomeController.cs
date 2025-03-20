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
        
        // show list of plays
        public IActionResult Index()
        {
            var plays = playService.GetAllPlays(); // Fetches list of plays
            return View(plays);
        }

        // show error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // display privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}