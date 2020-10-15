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
            var beer = new Beer
            {
                Name = beerDTO.Name,
                Description = beerDTO.Description,
                ABV = beerDTO.ABV,
                BreweryId = beerDTO.BreweryId,
                StyleId = beerDTO.StyleId,

            };

            var alreadyCreatedBeer = _context.Beers.Where(b => b.Name == beer.Name).FirstOrDefault();

            if (alreadyCreatedBeer.IsDeleted == true)
            {
                alreadyCreatedBeer.IsDeleted = false;
            }
            else if (alreadyCreatedBeer.IsDeleted == false)
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

        public IEnumerable<BeerDTO> FilterBeersByCountry(string name)
        {
            var breweries = _context.Breweries
                .Where(b => b.Country.Name == name)
                .ToList();

            var filtered = breweries
                .Select(b => b.Beers.GetDTO())
                .ToList();

            return (IEnumerable<BeerDTO>)filtered;
        }

        public IEnumerable<BeerDTO> FilterBeersByStyle(string name)
        {
            var beers = _context.Beers
              .Where(b => b.Style.Name == name)
              .ToList();

            return (IEnumerable<BeerDTO>)beers;
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
                Milliliters = beer.Milliliters,
                Description = beer.Description,
                BreweryId = beer.BreweryId,
                StyleId = beer.StyleId
            };

            return beerDTO;
        }

        public IEnumerable<BeerDTO> SortBeerByABV()
        {
            var beers = _context.Beers
                      .Where(b => b.IsDeleted == false)
                      .OrderBy(b => b.ABV).ToList();

            return (IEnumerable<BeerDTO>)beers;
        }

        public IEnumerable<BeerDTO> SortBeerByName()
        {
            var beers = _context.Beers
                     .Where(b => b.IsDeleted == false)
                     .OrderBy(b => b.Name).ToList();

            return (IEnumerable<BeerDTO>)beers;
        }
        public IEnumerable<BeerDTO> SortBeerByRating()
        {
            var beers = _context.Beers
                    .Where(b => b.IsDeleted == false)
                    .OrderBy(b => b.Rating).ToList();

            return (IEnumerable<BeerDTO>)beers;
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
            var beer = await Task.Run(() => this._context.Beers
            .Include(b => b.Name)
            .Include(b => b.ABV)
            .Include(b => b.Brewery)
            .Include(b => b.Rating)
            .Include(b => b.Milliliters)
            .Include(b => b.Description)
            .Include(b => b.Style)
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO()));

            _context.Beers.Add((Beer)beer);

            await _context.SaveChangesAsync();

            return (BeerDTO)beer;
        }

        public async Task<BeerDTO> DeleteBeerAsync(BeerDTO beerDTO)
        {
            var beer = await Task.Run(() => this._context.Beers
                .FirstOrDefaultAsync(x => x.Name == beerDTO.Name && x.IsDeleted == false));

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
        public async Task<BeerDTO> GetBeerAsync(int id)
        {
            var beer = await Task.Run(() => this._context.Beers
                .FirstOrDefaultAsync(beer => beer.Id == id));

            return beer.GetDTO();
        }

        public async Task<BeerDTO> UpdateBeerAsync(BeerDTO beerDTO, string newName)
        {
            var beer = await Task.Run(() => this._context.Beers
               .FirstOrDefaultAsync(x => x.Name == beerDTO.Name));

            beer.Name = newName;

            await _context.SaveChangesAsync();

            return beer.GetDTO();
        }
    }
}
