using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BeerOverflow.Controllers
{
    public class BeerController : Controller
    {
        private readonly IBeerService _service;
        private readonly IMapper _mapper;

        public BeerController(IBeerService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public IActionResult CreateBeer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBeer(CreateBeerViewModel createBeerViewModel)
        {
            var beerDTO = _mapper.Map<BeerDTO>(createBeerViewModel);

            _service.CreateBeer(beerDTO);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
