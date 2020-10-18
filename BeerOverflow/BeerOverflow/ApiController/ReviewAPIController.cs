using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerOverflow.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewAPIController : ControllerBase
    {
        private readonly IReviewService _service;
        public ReviewAPIController(IReviewService service)
        {
            this._service = service;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewDTO reviewDTO)
        {
            var review = await _service.CreateReviewAsync(reviewDTO);
            return new JsonResult(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _service.DeleteReviewAsync(id);
            return new JsonResult(review);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _service.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            try
            {
                var review = await _service.GetReviewAsync(id);
                return Ok(review);
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateReview([FromQuery] int id, [FromQuery] string newContent)
        {
            var review = await _service.UpdateReviewAsync(id, newContent);
            return new JsonResult(review);
        }
    }
}
