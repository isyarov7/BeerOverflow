﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeerOverflow.Services.Contracts;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using BeerOverflow.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace BeerOverflow.Controllers
{
    public class BreweriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IBreweryService _service;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public BreweriesController(SignInManager<User> signInManager,IBreweryService service, IMapper mapper, ICountryService countryService)
        {
            this._signInManager = signInManager;
            this._service = service;
            this._mapper = mapper;
            this._countryService = countryService;
        }

        // GET: Breweries
        public IActionResult Index()
        {
            return View( _service.GetAllBreweries());
        }

        // GET: Breweries/Details/5
        public IActionResult Details(int id)
        {

            var brewery = _service.GetBrewery(id);

            return View(brewery);
        }

        // // GET: Breweries/Create
        public async Task<IActionResult> Create()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            ViewData["CountryId"] = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name");
            return View();
        }

        // // POST: Breweries/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BreweryViewModel breweryViewModel)
        {
            var breweryDTO = _mapper.Map<BreweryDTO>(breweryViewModel);

            await _service.CreateBreweryAsync(breweryDTO);

            return RedirectToAction(nameof(Index));

        }

        // // GET: Breweries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            var brewery = _service.GetBrewery(id);

            ViewData["CountryId"] = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name");

            return View(brewery);
        }

        // // POST: Breweries/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BreweryViewModel breweryViewModel)
        {
            var breweryDTO = _mapper.Map<BreweryDTO>(breweryViewModel);

            await _service.UpdateBreweryAsync(id, breweryDTO);

            return RedirectToAction(nameof(Index));
        }

        // // GET: Breweries/Delete/5
        public IActionResult Delete(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            var brewery =_service.GetBrewery(id);

            return View(brewery);
        }

        // // POST: Breweries/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brewery = await _service.DeleteBreweryAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
