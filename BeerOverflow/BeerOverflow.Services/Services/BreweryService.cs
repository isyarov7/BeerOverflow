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
            if (!_context.Breweries.Any(b => b.Name == breweryDTO.Name))
            {
                _context.Breweries.Add(breweryDTO.GetBrewery());
            }
            else
            {
                var brewery = _context.Breweries.Where(b => b.Name == breweryDTO.Name).FirstOrDefault();
                brewery.IsDeleted = false;
            }


            await _context.SaveChangesAsync();

            return breweryDTO;
        }

        public async Task<BreweryDTO> DeleteBreweryAsync(string name)
        {
            var brewery = await Task.Run(() => this._context.Breweries
                         .Where(x => x.Name == name).FirstOrDefault());

            brewery.IsDeleted = true;

            await _context.SaveChangesAsync();

            return brewery.GetDTO();
        }

        public async Task<ICollection<BreweryDTO>> GetAllBreweriesAsync()
        {
            var breweries = await Task.Run(() => this._context.Breweries
           .Where(c => c.IsDeleted == false)
           .Select(b => b.GetDTO())
           .ToListAsync());

            return breweries;
        }

        public async Task<BreweryDTO> GetBreweryAsync(int id)
        {
            var brewery = await Task.Run(() => this._context.Breweries
                .FirstOrDefaultAsync(brewery => brewery.Id == id));

            return brewery.GetDTO();
        }

        public async Task<BreweryDTO> UpdateBreweryAsync(string oldName, string newName)
        {
            var brewery = await Task.Run(() => this._context.Breweries
               .FirstOrDefaultAsync(x => x.Name == oldName));

            brewery.Name = newName;

            await _context.SaveChangesAsync();

            return brewery.GetDTO();
        }
    }
}
