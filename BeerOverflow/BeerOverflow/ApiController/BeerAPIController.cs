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
    public class BeerAPIController : ControllerBase
    {
        private readonly IBeerService _service;
        public BeerAPIController(IBeerService service)
        {
            this._service = service;
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateBeer([FromBody] BeerDTO beerDTO)
        {
            var beer = await _service.CreateBeerAsync(beerDTO);
            return new JsonResult(beer);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteBeer(string name)
        {
            var beer = await _service.DeleteBeerAsync(name);
            return new JsonResult(beer);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBeers()
        {
            var beers = await _service.GetAllBeersAsync();
            return Ok(beers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeer(int id)
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

        [HttpPut("")]
        public async Task<IActionResult> UpdateBeer([FromQuery] string oldName, [FromQuery] string newName)
        {
            var beer = await _service.UpdateBeerAsync(oldName, newName);
            return new JsonResult(beer);
        }

        [HttpGet("sortbyname")]
        public async Task<IActionResult> SortByName()
        {
            var sortBeersByName = await _service.SortBeerByNameAsync();
            return new JsonResult(sortBeersByName);
        }

        [HttpGet("sortbyabv")]
        public async Task<IActionResult> SortByAbv()
        {
            var sortBeersByAbv = await _service.SortBeerByABVAsync();
            return new JsonResult(sortBeersByAbv);
        }

        [HttpGet("sortbyrating")]
        public async Task<IActionResult> SortByRating()
        {
            var sortBeersByRating = await _service.SortBeerByRatingAsync();
            return new JsonResult(sortBeersByRating);
        }

        [HttpGet("filterbycountry")]
        public async Task<IActionResult> FilterBeersByCountry([FromQuery] string name)
        {
            var filterBeersByCountry = await _service.FilterBeersByCountryAsync(name);
            return new JsonResult(filterBeersByCountry);
        }

        [HttpGet("filterbystyle")]

        public async Task<IActionResult> FilterBeersByStyle([FromQuery] string name)
        {
            var filterBeersByStyle = await _service.FilterBeersByStyleAsync(name);
            return new JsonResult(filterBeersByStyle);
        }
    }
}