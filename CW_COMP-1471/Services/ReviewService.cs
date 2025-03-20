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

            Guid paymentRefGuid;
            if (!Guid.TryParse(postReview.PaymentRefNumber, out paymentRefGuid))
            {
                throw new Exception($"Invalid Payment Reference Number: {postReview.PaymentRefNumber}");
            }

            Payment payment = await _context.Payments
                .Include(x => x.Booking)
                .FirstOrDefaultAsync(x => x.PaymentReferenceNumber == paymentRefGuid);

            if (payment.Booking.Tickets.Any(x => x.UserId == postReview.ReviewerId))
            {
                throw new Exception($"Invalid Payment Reference Number: {postReview.PaymentRefNumber}");
            }

            User reviewer = await _context.Users.FirstOrDefaultAsync(x => x.Id == postReview.ReviewerId);
            
            Review review = new Review{
                ReviewerName = reviewer.FirstName,
                PlayId = postReview.PlayId,
                Rating = postReview.Rating,
                Comment = postReview.Comment,
                ReviewDate = DateTime.UtcNow,
                ReviewerId = postReview.ReviewerId ?? 0,
            };

            await _context.Reviews.AddAsync(review);
            int rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected == 0)
            {
                throw new Exception("Failed to save the review.");
            }
        }
    }
}
