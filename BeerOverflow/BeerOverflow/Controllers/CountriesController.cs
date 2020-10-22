using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerOverflow.Services.Contracts;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.DTO;

namespace BeerOverflow.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryService _service;
        private readonly IMapper _mapper;

        public CountriesController(ICountryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCountriesAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var country = await _service.GetCountryAsync(id);

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel countryViewModel)
        {
            var countryDTO = _mapper.Map<CountryDTO>(countryViewModel);

            await _service.CreateCountryAsync(countryDTO);

            return RedirectToAction(nameof(Index));
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var country = await _service.GetCountryAsync(id);
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryViewModel countryViewModel)
        {
            var countryDTO = _mapper.Map<CountryDTO>(countryViewModel);

            await _service.UpdateCountryAsync(id, countryDTO);

            return RedirectToAction(nameof(Index));
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var country = await _service.GetCountryAsync(id);

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _service.DeleteCountryAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
