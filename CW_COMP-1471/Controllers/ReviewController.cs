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

        // save reviews
        [HttpPost]
        public async Task<IActionResult> AddReview([FromForm] PostReviewModel postReview)
        {
            try
            {
                int userId = (int)HttpContext.Session.GetInt32("LoggedInUser");
                postReview.ReviewerId = userId;
                await _reviewService.PostReview(postReview);
                return Redirect($"/api/play/Playdetails/{postReview.PlayId}");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Redirect($"/api/play/Playdetails/{postReview.PlayId}");
            }
        }
    }
}
