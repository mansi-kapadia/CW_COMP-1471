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
            bool IsVerifiedBuyer = false;
            Guid.TryParse(postReview.PaymentRefNumber, out paymentRefGuid);

            Payment payment = await _context.Payments
                .Include(x => x.Booking).ThenInclude(x => x.Tickets)
                .FirstOrDefaultAsync(x => x.PaymentReferenceNumber == paymentRefGuid);
            
            if (payment?.Booking?.Tickets != null && payment.Booking.Tickets.Any(x => x.UserId == postReview.ReviewerId))
            {
                IsVerifiedBuyer = true;
            }

            User reviewer = await _context.Users.FirstOrDefaultAsync(x => x.Id == postReview.ReviewerId);
            
            Review review = new Review{
                ReviewerName = reviewer.FirstName,
                PlayId = postReview.PlayId,
                Rating = postReview.Rating,
                Comment = postReview.Comment,
                ReviewDate = DateTime.UtcNow,
                ReviewerId = postReview.ReviewerId ?? 0,
                IsVerifiedBuyer = IsVerifiedBuyer
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
