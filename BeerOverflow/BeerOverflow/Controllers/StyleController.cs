using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerOverflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : Controller
    {
        private readonly IStyleService _service;
        private readonly IMapper _mapper;

        public StyleController(IStyleService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public IActionResult CreateStyle()
        {
            return View();
        }

       // [HttpPost]
       // public IActionResult CreateStyle(CreateStyleViewModel createstyleViewModel)
       // {
       //     var styleDTO = _mapper.Map<StyleDTO>(createStyleViewModel);
       //
       //     _service.CreateStyle(styleDTO);
       //
       //     return RedirectToAction("Index", "Home");
       // }

        public IActionResult Index()
        {
            return View();
        }
    }
}
