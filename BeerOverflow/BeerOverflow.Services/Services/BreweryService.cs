using System;
using System.Collections.Generic;
using System.Linq;
using BeerOverflow.Database;
using System.Threading.Tasks;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BeerOverflow.Services.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly BeerOverflowDbContext _context;
        public BreweryService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public async Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO)
        {
            if (_context.Breweries.Any(b => b.Name == breweryDTO.Name))
            {
                var oldBrewery = _context.Breweries.Where(b => b.Name == breweryDTO.Name).FirstOrDefault();
                _context.Breweries.Remove(oldBrewery);
            }

            _context.Breweries.Add(breweryDTO.GetBrewery());
            await _context.SaveChangesAsync();

            return breweryDTO;
        }

        public async Task<BreweryDTO> DeleteBreweryAsync(int id)
        {
            var brewery = await this._context.Breweries.
                Include( x => x.Country)
                         .FirstOrDefaultAsync(x => x.Id == id);

            brewery.IsDeleted = true;

            await _context.SaveChangesAsync();

            return brewery.GetDTO();
        }

        public ICollection<BreweryDTO> GetAllBreweries()
        {
            var breweries = this._context.Breweries
                .Include(x => x.Country)
           .Where(c => c.IsDeleted == false)
           .Select(b => b.GetDTO())
           .ToList();

            return breweries;
        }

        public BreweryDTO GetBrewery(int id)
        {
            var brewery =  this._context.Breweries
                .Include(x => x.Country)
                .FirstOrDefault(brewery => brewery.Id == id);

            return brewery.GetDTO();
        }

        public async Task<BreweryDTO> UpdateBreweryAsync(int id, BreweryDTO breweryDTO)
        {
            var brewery = await this._context.Breweries
            .Include(b => b.Country)
            .Where(x => x.Id == id).FirstOrDefaultAsync();

            _context.Breweries.Remove(brewery);

            var newBrewery = breweryDTO.GetBrewery();

            this._context.Breweries.Add(newBrewery);

            await _context.SaveChangesAsync();

            return brewery.GetDTO();
        }
    }
}
