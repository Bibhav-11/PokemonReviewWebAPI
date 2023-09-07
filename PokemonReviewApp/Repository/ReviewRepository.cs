using PokemonReviewApp.Data;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).First();
        }

        public Reviewer GetReviewer(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).Select(r => r.Reviewer).First();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }
    }
}
