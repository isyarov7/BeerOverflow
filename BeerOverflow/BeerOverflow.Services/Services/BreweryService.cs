using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
