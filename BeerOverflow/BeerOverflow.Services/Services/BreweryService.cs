using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var brewery = new Brewery
            {
                Name = breweryDTO.Name,
            };

            var alreadyCreated = _context.Breweries.Where(b => b.Name == brewery.Name).FirstOrDefault();
            
            if (alreadyCreated.IsDeleted == true)
            {
                alreadyCreated.IsDeleted = false;
            }
            else if (alreadyCreated.IsDeleted == false)
            {
                alreadyCreated.IsDeleted = false;
            }
            else
            {
                _context.Breweries.Add(brewery);
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

        public void UpdateBrewery(BreweryDTO breweryDTO, string name)
        {
            var brewery = _context.Breweries
               .Where(x => x.IsDeleted == false)
               .FirstOrDefault(x => x.Name == breweryDTO.Name);

            if (brewery == null)
            {
                throw new ArgumentNullException();
            }

            brewery.Name = name;

            _context.SaveChanges();
        }
        public async Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO)
        {
            var brewery = await Task.Run(() => this._context.Breweries
            .Include(b => b.Name)
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO()));

            _context.Breweries.Add((Brewery)brewery);

            await _context.SaveChangesAsync();

            return (BreweryDTO)brewery;
        }

        public async Task<BreweryDTO> DeleteBreweryAsync(BreweryDTO breweryDTO)
        {
            var brewery = await Task.Run(() => this._context.Breweries
                         .FirstOrDefaultAsync(x => x.Name == breweryDTO.Name && x.IsDeleted == false));

            brewery.IsDeleted = true;

            await _context.SaveChangesAsync();

            return brewery.GetDTO();
        }

        public async Task<ICollection<BreweryDTO>> GetAllBreweriesAsync()
        {
            var breweries = await Task.Run(() => this._context.Breweries
                    .Include(b => b.Name)
                    .Include(b => b.CountryId)
                    .Where(b => b.IsDeleted == false)
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

        public async Task<BreweryDTO> UpdateBreweryAsync(BreweryDTO breweryDTO, string newName)
        {
            var brewery = await Task.Run(() => this._context.Breweries
               .FirstOrDefaultAsync(x => x.Name == breweryDTO.Name));

            brewery.Name = newName;

            await _context.SaveChangesAsync();

            return brewery.GetDTO();
        }
    }
}
