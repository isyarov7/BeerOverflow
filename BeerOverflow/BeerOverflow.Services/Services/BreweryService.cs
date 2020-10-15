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

        public BreweryDTO GetBrewery(string name)
        {
            var brewery = _context.Breweries
                .Where(brewery => brewery.IsDeleted == false)
                .FirstOrDefault(brewery => brewery.Name == name).GetDTO();

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
            if (breweryDTO == null)
            {
                throw new ArgumentNullException();
            }

            var brewery = (await this._context.Breweries
                .FirstOrDefaultAsync(c => c.Name == breweryDTO.Name));

            var toRevive = _context.Breweries.Where(c => c.Name == brewery.Name && c.IsDeleted == true)
                .FirstOrDefault();

            if (_context.Breweries.Any(c => c.Name == brewery.Name && c.IsDeleted == true))
            {
                toRevive.IsDeleted = false;
            }
            else if (_context.Breweries.Any(c => c.Name == brewery.Name && c.IsDeleted == false))
            {
                toRevive.IsDeleted = false;
            }
            else
            {
                _context.Breweries.Add(brewery);
            }

            _context.SaveChanges();

            return brewery.GetDTO();
        }

        public async Task<BreweryDTO> DeleteBreweryAsync(BreweryDTO breweryDTO)
        {
            var brewery = (await this._context.Breweries
                .FirstOrDefaultAsync(x => x.Name == breweryDTO.Name && x.IsDeleted == false));

            if (brewery == null)
            {
                throw new ArgumentNullException();
            }

            brewery.IsDeleted = true;

            _context.SaveChanges();

            return brewery.GetDTO();
        }
        public async Task<BreweryDTO> GetAllBreweriesAsync()
        {
            var breweries = (await this._context.Breweries
                .FirstOrDefaultAsync(brewery => brewery.IsDeleted == false)).GetDTO();

            return breweries;
        }
        public async Task<BreweryDTO> GetBreweryAsync(int id)
        {
            var brewery = (await this._context.Breweries
                .FirstOrDefaultAsync(brewery => brewery.IsDeleted == false)).GetDTO();

            if (brewery == null)
            {
                throw new ArgumentException();
            }

            return brewery;
        }

        public async Task<BreweryDTO> UpdateBreweryAsync(BreweryDTO breweryDTO, string newName)
        {
            var brewery = (await this._context.Breweries
               .FirstOrDefaultAsync(x => x.Name == breweryDTO.Name)).GetDTO();

            if (brewery == null)
            {
                throw new ArgumentNullException();
            }

            brewery.Name = newName;

            _context.SaveChanges();

            return brewery;
        }
    }
}
