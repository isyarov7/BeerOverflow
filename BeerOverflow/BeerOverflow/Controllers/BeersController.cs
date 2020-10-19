using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;

namespace BeerOverflow.Controllers
{
    public class BeersController : Controller
    {
        private readonly BeerOverflowDbContext _context;

        public BeersController(BeerOverflowDbContext context)
        {
            _context = context;
        }

        // GET: Beers
        public async Task<IActionResult> Index()
        {
            var beerOverflowDbContext = _context.Beers.Include(b => b.Brewery)
                .Include(b => b.Style).Where(b => b.IsDeleted == false);
            return View(await beerOverflowDbContext.ToListAsync());
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.Brewery)
                .Include(b => b.Style)
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "Id", "Name");
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Description");
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ABV,Rating,BreweryId,ImageUrl,Milliliters,Description,StyleId")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                if (_context.Beers.Any(b => b.Name == beer.Name))
                {
                    var oldBeer = _context.Beers.FirstOrDefault(b=>b.Name == beer.Name);
                    _context.Beers.Remove(oldBeer);
                    await _context.SaveChangesAsync();
                }

                _context.Add(beer);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "Id", "Name", beer.BreweryId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Description", beer.StyleId);
            return View(beer);
        }

        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "Id", "Name", beer.BreweryId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Description", beer.StyleId);
            return View(beer);
        }

        // POST: Beers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ABV,Rating,BreweryId,ImageUrl,Milliliters,Description,StyleId")] Beer beer)
        {
            if (id != beer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "Id", "Name", beer.BreweryId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Description", beer.StyleId);
            return View(beer);
        }

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .Include(b => b.Brewery)
                .Include(b => b.Style)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            beer.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
            return _context.Beers.Any(e => e.Id == id);
        }
    }
}
