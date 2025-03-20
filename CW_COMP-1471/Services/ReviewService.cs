using CW_COMP_1471.Models;
using CW_COMP_1471.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CW_COMP_1471.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task PostReview(PostReviewModel postReview)
        {
            User reviewer = await _context.Users.FirstOrDefaultAsync(x => x.Id == postReview.ReviewerId);
            
            Review review = new Review{
                ReviewerName = reviewer.FirstName,
                PlayId = postReview.PlayId,
                Rating = postReview.Rating,
                Comment = postReview.Comment,
                ReviewDate = DateTime.Now,
            };

            _context.SaveChangesAsync();
        }
    }
}
