using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id)) return NotFound();
            var review = _reviewRepository.GetReview(id);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(review);
        }

        [HttpGet("{reviewId}/reviewer")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewer(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId)) return NotFound();
            var reviewer = _reviewRepository.GetReviewer(reviewId);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviewer);
        }
    }
}
