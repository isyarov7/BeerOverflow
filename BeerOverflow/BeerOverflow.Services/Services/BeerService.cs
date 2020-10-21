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
            var beers = await this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO())
            .ToListAsync();

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
        public async Task<BeerDTO> UpdateBeerAsync(BeerDTO beerDTO)
        {
            //Include-ват се само РЕЛАЦИИ
            var beer = await this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .Where(x => x.Id == id).FirstOrDefaultAsync();

            beer.Name = beerDTO.Name;
            beer.Description = beerDTO.Description;
            beer.Milliliters = beerDTO.Milliliters;
            beer.Rating = beerDTO.Rating;
            beer.ABV = beerDTO.ABV;
            beer.Brewery = beerDTO.Brewery;
            beer.Style = beerDTO.Style;

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