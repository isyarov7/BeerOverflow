using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using AutoMapper;
using BeerOverflow.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BeerOverflow.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace BeerOverflow.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeerService _service;
        private readonly IMapper _mapper;
        private readonly IBreweryService _breweryService;
        private readonly IStyleService _styleService;
        private readonly SignInManager<User> _signInManager;

        public BeersController(SignInManager<User> signInManager, IBeerService service, IMapper mapper, IBreweryService breweryService, IStyleService styleService)
        {
            this._signInManager = signInManager;
            this._service = service;
            this._mapper = mapper;
            this._breweryService = breweryService;
            this._styleService = styleService;
        }

        // GET: Beers
        public IActionResult Index()
        {
            return View( _service.GetAllBeers());
        }

        // GET: Beers/Details/5
        public IActionResult Details(int id)
        {
            var beer =_service.GetBeer(id);
            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            ViewData["BreweryId"] = new SelectList( _breweryService.GetAllBreweries(), "Id", "Name");
            ViewData["StyleId"] = new SelectList( _styleService.GetAllStyles(), "Id", "Name");
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel beer)
        {

            var beerDTO = _mapper.Map<BeerDTO>(beer);

            await _service.CreateBeerAsync(beerDTO);

            return RedirectToAction(nameof(Index));
        }


        //// GET: Beers/Edit/5
        public IActionResult Edit(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            var beer = _service.GetBeer(id);

            ViewData["BreweryId"] = new SelectList(_breweryService.GetAllBreweries(), "Id", "Name");
            ViewData["StyleId"] = new SelectList(_styleService.GetAllStyles(), "Id", "Name");

            return View(beer);
        }
        [Authorize]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BeerViewModel beerViewModel)
        {
            var beerDTO = _mapper.Map<BeerDTO>(beerViewModel);

            await _service.UpdateBeerAsync(id, beerDTO);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            var beer = _service.GetBeer(id);

            return View(beer);
        }
        // POST: Beers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _service.DeleteBeerAsync(id);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult FilterByCountry()
        {
            var vm = new FilterViewModel();
            vm.SearchText = "";
            vm.SearchResults = new List<BeerDTO>();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCountry(FilterViewModel model)
        {
            var result = await _service.FilterBeersByCountryAsync(model.SearchText);
            model.SearchResults = result;
            return View(model);
        }

        [HttpGet]
        public IActionResult FilterByStyle()
        {
            var vm = new FilterViewModel();
            vm.SearchText = "";
            vm.SearchResults = new List<BeerDTO>();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByStyle(FilterViewModel model)
        {
            var result = await _service.FilterBeersByStyleAsync(model.SearchText);
            model.SearchResults = result;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SortByName()
        {
            var result = await _service.SortBeerByNameAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> SortByABV()
        {
            var result = await _service.SortBeerByABVAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> SortByRating()
        {
            var result = await _service.SortBeerByRatingAsync();
            return View(result);
        }
    }
}