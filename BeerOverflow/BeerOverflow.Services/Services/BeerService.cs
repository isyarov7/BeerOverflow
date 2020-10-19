using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using Microsoft.EntityFrameworkCore;

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
            var beer = new Beer
            {
                Name = beerDTO.Name,
                ABV = beerDTO.ABV,
                BreweryId = beerDTO.BreweryId,
                Milliliters = beerDTO.Milliliters,
                Rating = beerDTO.Rating,
                Description = beerDTO.Description,
                StyleId = beerDTO.StyleId
            };

            var alreadyCreatedBeer = _context.Beers.Where(b => b.Name == beer.Name).FirstOrDefault();

            if (_context.Beers.Any(c => c.Name == beer.Name && c.IsDeleted == true))
            {
                alreadyCreatedBeer.IsDeleted = false;
            }
            else if (_context.Beers.Any(c => c.Name == beer.Name && c.IsDeleted == false))
            {
                alreadyCreatedBeer.IsDeleted = false;
            }
            else
            {
                _context.Beers.Add(beer);
            }

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
        public void UpdateBeer(string oldName, string newName)
        {
            var beer = _context.Beers
                      .Where(x => x.IsDeleted == false)
                      .FirstOrDefault(x => x.Name == oldName);

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            beer.Name = newName;
        }

        public ICollection<BeerDTO> FilterBeersByCountry(string name)
        {
            var breweries = _context.Breweries
                .Where(b => b.Country.Name == name)
                .ToList();

            var filtered = breweries
                .Select(b => b.Beers.GetDTO())
                .ToList();

            return (ICollection<BeerDTO>)filtered;
        }

        public ICollection<BeerDTO> FilterBeersByStyle(string name)
        {
            var beers = _context.Beers
              .Where(b => b.Style.Name == name)
              .ToList();

            return (ICollection<BeerDTO>)beers;
        }

        public ICollection<BeerDTO> GetAllBeers()
        {
            var beers = _context.Beers
                .Where(beer => beer.IsDeleted == false)
                .Include(beer => new BeerDTO
                {
                    Name = beer.Name,
                    ABV = beer.ABV,
                    Milliliters = beer.Milliliters,
                    Rating = beer.Rating,
                    Description = beer.Description,
                    BreweryId = beer.BreweryId,
                    StyleId = beer.StyleId
                })
               .ToList()
               .GetDTO();

            return beers;
        }

        public BeerDTO GetBeer(string name)
        {
            var beer = _context.Beers
                 .Where(beer => beer.IsDeleted == false)
                 .FirstOrDefault(beer => beer.Name == name).GetDTO();

            if (beer == null)
            {
                throw new ArgumentNullException();
            }

            var beerDTO = new BeerDTO
            {
                Name = beer.Name,
                ABV = beer.ABV,
                BreweryId = beer.BreweryId,
                Milliliters = beer.Milliliters,
                Rating = beer.Rating,
                Description = beer.Description,
                StyleId = beer.StyleId
            };

            return beerDTO;
        }

        public ICollection<BeerDTO> SortBeersByABV()
        {
            var beers = _context.Beers
                      .Where(b => b.IsDeleted == false)
                      .OrderBy(b => b.ABV).ToList();

            return (ICollection<BeerDTO>)beers;
        }

        public ICollection<BeerDTO> SortBeersByName()
        {
            var beers = _context.Beers
                     .Where(b => b.IsDeleted == false)
                     .OrderBy(b => b.Name).ToList();

            return (ICollection<BeerDTO>)beers;
        }
        public ICollection<BeerDTO> SortBeersByRating()
        {
            var beers = _context.Beers
                    .Where(b => b.IsDeleted == false)
                    .OrderBy(b => b.Rating).ToList();

            return (ICollection<BeerDTO>)beers;
        }
        //Async Methods
        public async Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO)
        {
            if (!_context.Beers.Any(b => b.Name == beerDTO.Name))
            {
                _context.Beers.Add(beerDTO.GetBeer());
            }
            else
            {
                var beer = _context.Beers.Where(b => b.Name == beerDTO.Name).FirstOrDefault();
                beer.IsDeleted = false;
            }


            await _context.SaveChangesAsync();

            return beerDTO;
        }
        //ok
        public async Task<BeerDTO> DeleteBeerAsync(string name)
        {
            var beer = await Task.Run(() => this._context.Beers
                .FirstOrDefaultAsync(x => x.Name == name && x.IsDeleted == false));

            beer.IsDeleted = true;

            await _context.SaveChangesAsync();

            return beer.GetDTO();
        }

        public async Task<ICollection<BeerDTO>> GetAllBeersAsync()
        {
            var beers = await Task.Run(() => this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO())
            .ToListAsync());

            return beers;
        }
        //Ok
        public async Task<BeerDTO> GetBeerAsync(int id)
        {
            var beer = await Task.Run(() => this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .FirstOrDefaultAsync(beer => beer.Id == id));

            return beer.GetDTO();
        }
        //OK
        public async Task<BeerDTO> UpdateBeerAsync(string oldName, string newName)
        {
            //Include-ват се само РЕЛАЦИИ
            var beer = await Task.Run(() => this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .FirstOrDefaultAsync(x => x.Name == oldName));

            beer.Name = newName;

            this._context.Update(beer);

            await _context.SaveChangesAsync();

            return beer.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> FilterBeersByCountryAsync(string name)
        {
            var breweries = await Task.Run(() => _context.Breweries
                 .Include(b => b.Country)
                 .Where(c => c.Country.Name == name)
                 .ToList());

            var beers = breweries
                .Select(b => b.Beers.GetDTO())
                .ToList();

            return (ICollection<BeerDTO>)beers;
        }
        //Ok
        public async Task<ICollection<BeerDTO>> FilterBeersByStyleAsync(string name)
        {
            var beers = await Task.Run(() => _context.Beers
              .Where(b => b.Style.Name == name)
              .ToList());

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> SortBeerByNameAsync()
        {
            var beers = await Task.Run(() => _context.Beers
                     .Where(b => b.IsDeleted == false)
                     .OrderBy(b => b.Name).ToList());

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> SortBeerByABVAsync()
        {
            var beers = await Task.Run(() => _context.Beers
                      .Where(b => b.IsDeleted == false)
                      .OrderBy(b => b.ABV).ToList());

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> SortBeerByRatingAsync()
        {
            var beers = await Task.Run(() => _context.Beers
                    .Where(b => b.IsDeleted == false)
                    .OrderByDescending(b => b.Rating).ToList());

            return beers.GetDTO();
        }
    }
}