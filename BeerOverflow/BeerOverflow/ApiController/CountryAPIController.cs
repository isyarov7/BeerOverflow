using System;
using System.Threading.Tasks;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BeerOverflow.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryAPIController : ControllerBase
    {
        private readonly ICountryService _service;
        public CountryAPIController(ICountryService service)
        {
            this._service = service;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCountry([FromBody] CountryDTO countryDTO)
        {
            var country = await _service.CreateCountryAsync(countryDTO);
            return new JsonResult(country);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _service.DeleteCountryAsync(id);
            return new JsonResult(country);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _service.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _service.GetCountryAsync(id);
                return Ok(country);
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateCountry([FromQuery] int id, [FromQuery] CountryDTO countryDTO)
        {
            var country = await _service.UpdateCountryAsync(id, countryDTO);
            return new JsonResult(country);
        }
    }
}