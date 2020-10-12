using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BeerOverflow.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _service;
        private readonly IMapper _mapper;

        public CountryController(ICountryService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        public IActionResult CreateCountry()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult CreateCountry(CountryViewModel countryViewModel)
        {
            var countryDTO = _mapper.Map<CountryDTO>(countryViewModel);

            _service.CreateCountry(countryDTO);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
