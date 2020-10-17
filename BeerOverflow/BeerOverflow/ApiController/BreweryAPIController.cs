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

        public async Task<IActionResult> CreateBrewery([FromBody] BreweryDTO breweryDTO)
        {
            var brewery = await _service.CreateBreweryAsync(breweryDTO);
            return new JsonResult(brewery);
        }
        //DO NOT WORK
        [HttpPost("name={name}")]
        public async Task<IActionResult> DeleteBreweryAsync([FromQuery] string name)
        {
            var brewery = await _service.DeleteBreweryAsync(name);
            return new JsonResult(brewery);
        }
        //Ok
        [HttpGet("")]
        public async Task<IActionResult> GetAllBreweries()
        {
            var breweries = await _service.GetAllBreweriesAsync();
            return Ok(breweries);
        }
        //Ok
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetBreweryAsync(int id)
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
        //Ok
        [HttpPut("")]
        public async Task<IActionResult> UpdateBrewery([FromQuery] string oldName, [FromQuery] string newName)
        {
            var brewery = await _service.UpdateBreweryAsync(oldName, newName);
            return new JsonResult(brewery);
        }
    }
}
