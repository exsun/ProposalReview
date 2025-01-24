using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewsController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Scoring>> AddReview([FromBody] Scoring scoring)
        {
            try
            {
                var newScoring = await _reviewService.AddReviewAsync(scoring.ProposalId, scoring);
                return CreatedAtAction("AddReview", new { id = newScoring.Id }, newScoring);
            }
            catch
            {
                return BadRequest("Error adding review.");
            }
        }

        [HttpPut("finalize/{id}")]
        public async Task<IActionResult> FinalizeReview(int id)
        {
            try
            {
                var finalizedReview = await _reviewService.FinalizeReviewAsync(id);
                return Ok(finalizedReview);
            }
            catch
            {
                return NotFound("Review not found.");
            }
        }
    }
}
