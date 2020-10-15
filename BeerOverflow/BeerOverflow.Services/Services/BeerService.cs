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
    public class BeerService : IBeerService
    {
        private readonly BeerOverflowDbContext _context;
        public BeerService(BeerOverflowDbContext context)
        {
            this._context = context;
        }
        public void CreateBeer(BeerDTO beerDTO)
        {
            if (beerDTO == null)
            {
                throw new ArgumentNullException();
            }

            else if (_context.Beers.Any(b => b.Name == beerDTO.Name))
            {
                throw new ArgumentException("Beer with such name already exists!");
            }

            var beer = new Beer
            {
                Name = beerDTO.Name,
                ABV = beerDTO.ABV,
                Milliliters = beerDTO.Milliliters,
                Description = beerDTO.Description,
                BreweryId = beerDTO.BreweryId,
                StyleId = beerDTO.StyleId
            };

            _context.Beers.Add(beer);

            _context.SaveChanges();
        }

        public void DeleteBeer(BeerDTO beerDTO)
        {
            var beer = _context.Beers
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Name == beerDTO.Name);

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            beer.IsDeleted = true;

            _context.SaveChanges();
        }
        //TODO
        public IEnumerable<BeerDTO> FilterBeersByCountry()
        {
            throw new NotImplementedException();
        }
        //TODO
        public IEnumerable<BeerDTO> FilterBeersByStyle()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BeerDTO> GetAllBeers()
        {
            var beers = _context.Beers
                .Where(beer => beer.IsDeleted == false)
                .Include(beer => new BeerDTO
                {
                    Name = beer.Name,
                    ABV = beer.ABV,
                    Milliliters = beer.Milliliters,
                    Description = beer.Description,
                    BreweryId = beer.BreweryId,
                    StyleId = beer.StyleId
                })
               .ToList()
               .GetDTO();

            return beers;
        }

        public BeerDTO GetBeer(int id)
        {
            var beer = _context.Beers
                 .Where(beer => beer.IsDeleted == false)
                 .FirstOrDefault(beer => beer.Id == id).GetDTO();

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            var beerDTO = new BeerDTO
            {
                Name = beer.Name,
                ABV = beer.ABV,
                Milliliters = beer.Milliliters,
                Description = beer.Description,
                BreweryId = beer.BreweryId,
                StyleId = beer.StyleId
            };

            return beerDTO;
        }
        //TODO
        public IEnumerable<BeerDTO> SortBeerByABV()
        {
            throw new NotImplementedException();
        }
        //TODO
        public IEnumerable<BeerDTO> SortBeerByName()
        {
            throw new NotImplementedException();
        }
        //TODO
        public IEnumerable<BeerDTO> SortBeerByRating()
        {
            throw new NotImplementedException();
        }

        public void UpdateBeer(int id, BeerDTO beerDTO)
        {
            var beer = _context.Beers.Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            beer.Name = beerDTO.Name;
            beer.ABV = beerDTO.ABV;
            beer.Milliliters = beerDTO.Milliliters;
            beer.Description = beerDTO.Description;
            beer.BreweryId = beerDTO.BreweryId;
            beer.StyleId = beerDTO.StyleId;

            _context.SaveChanges();
        }
        public async Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO)
        {
            if (beerDTO == null)
            {
                throw new ArgumentNullException();
            }

            var beer = (await this._context.Beers
                .FirstOrDefaultAsync(c => c.Name == beerDTO.Name));

            var toRevive = _context.Beers.Where(c => c.Name == beer.Name && c.IsDeleted == true)
                .FirstOrDefault();

            if (_context.Beers.Any(c => c.Name == beer.Name && c.IsDeleted == true))
            {
                toRevive.IsDeleted = false;
            }
            else if (_context.Beers.Any(c => c.Name == beer.Name && c.IsDeleted == false))
            {
                toRevive.IsDeleted = false;
            }
            else
            {
                _context.Beers.Add(beer);
            }

            _context.SaveChanges();

            return beer.GetDTO();
        }

        public async Task<BeerDTO> DeleteBeerAsync(BeerDTO beerDTO)
        {
            var beer = (await this._context.Beers
                .FirstOrDefaultAsync(x => x.Name == beerDTO.Name && x.IsDeleted == false));

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            beer.IsDeleted = true;

            _context.SaveChanges();

            return beer.GetDTO();
        }
        public async Task<BeerDTO> GetAllBeersAsync()
        {
            var beers = (await this._context.Beers
                .FirstOrDefaultAsync(beer => beer.IsDeleted == false)).GetDTO();

            return beers;
        }
        public async Task<BeerDTO> GetBeerAsync(int id)
        {
            var beer = (await this._context.Beers
                .FirstOrDefaultAsync(beer => beer.IsDeleted == false)).GetDTO();

            if (beer == null)
            {
                throw new ArgumentException();
            }

            return beer;
        }

        public async Task<BeerDTO> UpdateBeerAsync(BeerDTO beerDTO, string newName)
        {
            var beer = (await this._context.Beers
               .FirstOrDefaultAsync(x => x.Name == beerDTO.Name)).GetDTO();

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            beer.Name = newName;

            _context.SaveChanges();

            return beer;
        }
    }
}
