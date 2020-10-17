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
    public class BreweryController : Controller
    {
        private readonly IBreweryService _service;
        private readonly IMapper _mapper;

        public BreweryController(IBreweryService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public IActionResult CreateBrewery()
        {
            return View();
        }

       // [HttpPost]
       // public IActionResult CreateBrewery(CreateBreweryViewModel createBreweryViewModel)
       // {
       //     var breweryDTO = _mapper.Map<BreweryDTO>(createBreweryViewModel);
       //
       //     _service.CreateBrewery(breweryDTO);
       //
       //     return RedirectToAction("Index", "Home");
       // }

        public IActionResult Index()
        {
            return View();
        }
    }
}
