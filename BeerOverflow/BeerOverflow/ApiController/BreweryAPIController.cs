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
    public class BreweryAPIController : ControllerBase
    {
        private readonly IBreweryService _service;
        public BreweryAPIController(IBreweryService service)
        {
            this._service = service;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateBrewery([FromBody] BreweryDTO breweryDTO)
        {
            var brewery = await _service.CreateBreweryAsync(breweryDTO);
            return new JsonResult(breweryDTO);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteBrewery(int id)
        {
            var brewery = await _service.DeleteBreweryAsync(id);
            return new JsonResult(brewery);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBreweries()
        {
            var breweries = await _service.GetAllBreweriesAsync();
            return Ok(breweries);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetBrewery(int id)
        {
            try
            {
                var brewery = await _service.GetBreweryAsync(id);
                return Ok(brewery);
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateBrewery([FromQuery] int id, [FromQuery] BreweryDTO breweryDTO)
        {
            var brewery = await _service.UpdateBreweryAsync(id, breweryDTO);
            return new JsonResult(brewery);
        }
    }
}