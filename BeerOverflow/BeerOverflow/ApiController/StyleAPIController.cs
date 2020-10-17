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

        public async Task<IActionResult> CreateStyle([FromBody] StyleDTO styleDTO)
        {
            var style = await _service.CreateStyleAsync(styleDTO);
            return new JsonResult(style);
        }
        //DO NOT WORK
        //[HttpPost("name={name}")]
        //public async Task<IActionResult> DeleteStyleAsync([FromQuery] string name)
        //{
        //    var style = await _service.DeleteStyleAsync(name);
        //    return new JsonResult(style);
        //}
        //Ok
        [HttpGet("")]
        public async Task<IActionResult> GetAllStyles()
        {
            var styles = await _service.GetAllStylesAsync();
            return Ok(styles);
        }
        //Ok
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetStyleAsync(int id)
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
        //Ok
        //[HttpPut("")]
        //public async Task<IActionResult> UpdateStyle([FromQuery] string oldName, [FromQuery] string newName)
        //{
        //    var style = await _service.UpdateStyleAsync(oldName, newName);
        //    return new JsonResult(style);
        //}
    }
}
