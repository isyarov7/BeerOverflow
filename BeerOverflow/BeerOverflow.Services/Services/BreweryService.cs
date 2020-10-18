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

        public void CreateBrewery(BreweryDTO breweryDTO)
        {
            if (breweryDTO == null)
            {
                throw new ArgumentException();
            }

            var brewery = new Brewery
            {
                Name = breweryDTO.Name,
            };

            if (_context.Breweries.Any(b => b.Name == brewery.Name))
            {
                throw new ArgumentException("Brewery with such name already exists!");
            }

            _context.Breweries.Add(brewery);

            _context.SaveChanges();
        }

        public void DeleteBrewery(BreweryDTO breweryDTO)
        {
            var brewery = _context.Breweries
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Name == breweryDTO.Name);

            if (brewery == null)
            {
                throw new ArgumentNullException();
            }

            brewery.IsDeleted = true;

            _context.SaveChanges();
        }

        public IEnumerable<BreweryDTO> GetAllBreweries()
        {
            var breweries = _context.Breweries
                .Where(brewery => brewery.IsDeleted == false)
                .Include(brewery => new BreweryDTO
                {
                    Name = brewery.Name,
                })
               .ToList()
               .GetDTO();

            return breweries;
        }

        public BreweryDTO GetBrewery(int id)
        {
            var brewery = _context.Breweries
                .Where(brewery => brewery.IsDeleted == false)
                .FirstOrDefault(brewery => brewery.Id == id).GetDTO();

            if (brewery == null)
            {
                throw new ArgumentNullException();
            }

            var breweryDTO = new BreweryDTO
            {
                Name = brewery.Name,
            };

            return breweryDTO;
        }

        public void UpdateBrewery(int id, BreweryDTO breweryDTO)
        {
            var brewery = _context.Breweries.Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (brewery == null)
            {
                throw new ArgumentNullException();
            }

            brewery.Name = breweryDTO.Name;

            _context.SaveChanges();
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
