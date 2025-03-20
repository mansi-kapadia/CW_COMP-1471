using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services.Interface
{
    public interface IReviewService
    {
        Task PostReview(PostReviewModel postReview);
    }
}
