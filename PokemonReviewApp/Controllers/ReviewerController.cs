using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interface;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewerController(IReviewerRepository repository)
        {
            _reviewerRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviwers = _reviewerRepository.GetReviewers();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(reviwers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewer(int id)
        {
            if(!_reviewerRepository.ReviewerExists(id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var reviewer = _reviewerRepository.GetReviewer(id);
            return Ok(reviewer);
        }

        [HttpGet("{id}/reviews")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public IActionResult GetReviewsFromAReviewer(int id)
        {
            if (!_reviewerRepository.ReviewerExists(id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var reviews = _reviewerRepository.GetReviewsOfAReviewer(id);
            return Ok(reviews);
        }
    }
}
