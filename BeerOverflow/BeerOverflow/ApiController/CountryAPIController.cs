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

        public async Task<IActionResult> CreateCountry([FromBody] CountryDTO countryDTO)
        {
            var country = await _service.CreateCountryAsync(countryDTO);
            return new JsonResult(country);
        }
        //DO NOT WORK
        //[HttpPost("name={name}")]
        //public async Task<IActionResult> DeleteCountryAsync([FromQuery] string name)
        //{
        //    var country = await _service.DeleteCountryAsync(name);
        //    return new JsonResult(country);
        //}
        //Ok
        [HttpGet("")]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _service.GetAllCountriesAsync();
            return Ok(countries);
        }
        //Ok
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetCountryAsync(int id)
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
        //Ok
        //[HttpPut("")]
       // public async Task<IActionResult> UpdateCountry([FromQuery] string oldName, [FromQuery] string newName)
       // {
       //     var country = await _service.UpdateCountryAsync(oldName, newName);
       //     return new JsonResult(country);
       // }
    }
}
