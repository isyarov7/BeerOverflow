using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeerOverflow.Services.Contracts;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;

namespace BeerOverflow.Controllers
{
    public class BreweriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IBreweryService _service;
        private readonly IMapper _mapper;

        public BreweriesController(IBreweryService service, IMapper mapper, ICountryService countryService)
        {
            _service = service;
            _mapper = mapper;
            _countryService = countryService;
        }

        // GET: Breweries
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllBreweriesAsync());
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var brewery = await _service.GetBreweryAsync(id);

            return View(brewery);
        }

        // // GET: Breweries/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CountryId"] = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name");
            return View();
        }

        // // POST: Breweries/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            var brewery = await _service.GetBreweryAsync(id);

            ViewData["CountryId"] = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name");

            return View(brewery);
        }
        
        // // POST: Breweries/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, BreweryViewModel breweryViewModel)
         {
            var breweryDTO = _mapper.Map<BreweryDTO>(breweryViewModel);

            await _service.UpdateBreweryAsync(id, breweryDTO);

            return RedirectToAction(nameof(Index));
        }
         
        // // GET: Breweries/Delete/5
         public async Task<IActionResult> Delete(int id)
         {
            var brewery = await _service.DeleteBreweryAsync(id);

            return View(brewery);
        }
        
        // // POST: Breweries/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
            var brewery = await _service.DeleteBreweryAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
