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
            _reviewService = reviewService;
        }

        public IActionResult Index(int PlayId)
        {
            ViewBag.PlayId = PlayId;
            return View();
        }

        public async Task PostReviewAsync(PostReviewModel postReview)
        {
            await _reviewService.PostReview(postReview);
        }
    }
}
