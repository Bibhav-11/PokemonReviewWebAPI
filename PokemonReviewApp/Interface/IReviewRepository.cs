using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interface
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        Reviewer GetReviewer(int reviewId);
        bool ReviewExists(int id);
    }
}
