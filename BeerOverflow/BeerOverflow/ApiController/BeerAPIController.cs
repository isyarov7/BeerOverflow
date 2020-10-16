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
    [Route("api/beer")]
    [ApiController]
    public class BeerAPIController : ControllerBase
    {
        private readonly IBeerService _service;
        public BeerAPIController(IBeerService service)
        {
            this._service = service;
        }
        //DO NOT WORK
        [HttpPost("")]
        public async Task<IActionResult> CreateBeer([FromBody] BeerDTO beerDTO)
        {
            var beer = await _service.CreateBeerAsync(beerDTO);
            return new JsonResult(beer);
        }
        //DO NOT WORK
        [HttpPost("name={name}")]
        public async Task<IActionResult> DeleteBeerAsync([FromQuery] string name)
        {
            var beer = await _service.DeleteBeerAsync(name);
            return new JsonResult(beer);
        }
        //Ok
        [HttpGet("")]
        public async Task<IActionResult> GetAllBeers()
        {
            var beers = await _service.GetAllBeersAsync();
            return Ok(beers);
        }
        //Ok
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetBeerAsync(int id)
        {
            try
            {
                var beer = await _service.GetBeerAsync(id);
                return Ok(beer);
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }
        //Ok
        [HttpPut("")]
        public async Task<IActionResult> UpdateBeer([FromQuery] string oldName, [FromQuery] string newName)
        {
            var beer = await _service.UpdateBeerAsync(oldName, newName);
            return new JsonResult(beer);
        }
        //ok
        [HttpGet("sortbyname")]
        public async Task<IActionResult> SortByName()
        {
            var sortBeersByName = await _service.SortBeerByNameAsync();
            return new JsonResult(sortBeersByName);
        }
        //ok
        [HttpGet("sortbyabv")]
        public async Task<IActionResult> SortByAbv()
        {
            var sortBeersByAbv = await _service.SortBeerByABVAsync();
            return new JsonResult(sortBeersByAbv);
        }
        //ok
        [HttpGet("sortbyrating")]
        public async Task<IActionResult> SortByRating()
        {
            var sortBeersByRating = await _service.SortBeerByRatingAsync();
            return new JsonResult(sortBeersByRating);
        }
    }
}