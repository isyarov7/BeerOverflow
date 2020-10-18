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
    public class StyleAPIController : ControllerBase
    {
        private readonly IStyleService _service;
        public StyleAPIController(IStyleService service)
        {
            this._service = service;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateStyle([FromBody] StyleDTO styleDTO)
        {
            var style = await _service.CreateStyleAsync(styleDTO);
            return new JsonResult(style);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteStyle(string name)
        {
            var style = await _service.DeleteStyleAsync(name);
            return new JsonResult(style);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCountries()
        {
            var styles = await _service.GetAllStylesAsync();
            return Ok(styles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStyle(int id)
        {
            try
            {
                var style = await _service.GetStyleAsync(id);
                return Ok(style);
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateStyle([FromQuery] string oldName, [FromQuery] string newName)
        {
            var country = await _service.UpdateStyleAsync(oldName, newName);
            return new JsonResult(country);
        }
    }
}
}
