using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
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
        public async Task<IActionResult> DeleteStyle(int id)
        {
            var style = await _service.DeleteStyleAsync(id);
            return new JsonResult(style);
        }

        [HttpGet("")]
        public IActionResult GetAllCountries()
        {
            var styles = _service.GetAllStyles();
            return Ok(styles);
        }

        [HttpGet("{id}")]
        public IActionResult GetStyle(int id)
        {
            try
            {
                var style = _service.GetStyle(id);
                return Ok(style);
            }
            catch (Exception)
            {
                return this.NotFound();
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateStyle([FromQuery] int id, [FromQuery] StyleDTO styleDTO)
        {
            var country = await _service.UpdateStyleAsync(id, styleDTO);
            return new JsonResult(country);
        }
    }
}