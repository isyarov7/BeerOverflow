using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeerOverflow.Models;
using BeerOverflow.Services.Contracts;
using AutoMapper;
using System.Linq;
using System;

namespace BeerOverflow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBeerService _beerService;
        private readonly IBreweryService _breweryService;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IBeerService beerService, IMapper mapper,
            IBreweryService breweryService)
        {
            _logger = logger;
            _beerService = beerService;
            _mapper = mapper;
            _breweryService = breweryService;
        }
        // GET: HomeController
        public ActionResult Index()
        {

            var homeViewModel = new HomeIndexViewModel();

            var topRatedBeers = _beerService.GetAllBeers()
                .OrderByDescending(x => x.ABV)
                .Take(6)
                .Select(x => _mapper.Map<BeerViewModel>(x));
            homeViewModel.TopRatedBeers = topRatedBeers.ToList();

            return View(homeViewModel);
        }


        public IActionResult TopBeers()
        {
            var beers = _beerService.GetAllBeers();

            var beersView = _mapper.Map<BeerViewModel>(beers);

            return View(beersView);
        }


        public IActionResult TopBreweries()
        {
            var brewery = _breweryService.GetAllBreweries();

            var breweriesVM = _mapper.Map<BreweryViewModel>(brewery);

            return View(breweriesVM);
        }


        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var beer = _beerService.GetBeer(id);
                var brewery = _breweryService.GetBrewery(id);


                var breweryView = _mapper.Map<BreweryViewModel>(brewery);
                var beerViewMOdel = _mapper.Map<BeerViewModel>(beer);

                if (breweryView != null)
                {
                    return View(breweryView);
                }
                else
                {
                    return View(beerViewMOdel);
                }

            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }
    }
}
