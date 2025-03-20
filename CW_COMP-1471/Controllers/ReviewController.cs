using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CW_COMP_1471.Controllers
{

    public class ReviewController : Controller
    {
        public readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            // dependency injection
            _reviewService = reviewService;
        }

        // get reviews by play id
        public IActionResult Index(int PlayId)
        {
            ViewBag.PlayId = PlayId;
            return View();
        }

        // save reviews
        public async Task PostReviewAsync(PostReviewModel postReview)
        {
            await _reviewService.PostReview(postReview);
        }
    }
}
