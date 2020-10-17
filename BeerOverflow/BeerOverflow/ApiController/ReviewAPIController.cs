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

        public async Task<IActionResult> CreateReview([FromBody] ReviewDTO reviewDTO)
        {
            var review = await _service.CreateReviewAsync(reviewDTO);
            return new JsonResult(review);
        }
        //DO NOT WORK
        [HttpPost("name={name}")]
        public async Task<IActionResult> DeleteReviewAsync([FromQuery] string content)
        {
            var contents = await _service.DeleteReviewAsync(content);
            return new JsonResult(contents);
        }
        //Ok
        [HttpGet("")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _service.GetAllReviewsAsync();
            return Ok(reviews);
        }
        //Ok
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetReviewAsync(int id)
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
        //Ok
        [HttpPut("")]
        public async Task<IActionResult> UpdateReview([FromQuery] string oldContent, [FromQuery] string newContent)
        {
            var review = await _service.UpdateReviewAsync(oldContent, newContent);
            return new JsonResult(review);
        }
    }
}
