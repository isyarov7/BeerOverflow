using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflow.Database;
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
            if (_context.Beers.Any(b => b.Name == beerDTO.Name))
            {
                var oldBeer = _context.Beers.Where(b => b.Name == beerDTO.Name).FirstOrDefault();
                _context.Beers.Remove(oldBeer);
            }

            _context.Beers.Add(beerDTO.GetBeer());

            await _context.SaveChangesAsync();

            return beerDTO;
        }
        //ok
        public async Task<BeerDTO> DeleteBeerAsync(int id)
        {
            var beer = await this._context.Beers
                .Include(x =>x.Brewery)
                .Include(x=>x.Style)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            beer.IsDeleted = true;

            await _context.SaveChangesAsync();

            return beer.GetDTO();
        }

        public ICollection<BeerDTO> GetAllBeers()
        {
            var beers = this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO())
            .ToList();

            return beers;
        }
        //Ok
        public BeerDTO GetBeer(int id)
        {
            var beer = this._context.Beers
            .Include(b => b.Brewery)
            .Include(b => b.Style)
            .FirstOrDefault(beer => beer.Id == id);

            return beer.GetDTO();
        }
        //OK
        public async Task<BeerDTO> UpdateBeerAsync(int id, BeerDTO beerDTO)
        {
            var beer = await this._context.Beers
           .Include(b => b.Brewery)
           .Include(b => b.Style)
           .Where(x => x.Id == id).FirstOrDefaultAsync();

            _context.Beers.Remove(beer);

            var newBeer = beerDTO.GetBeer();

            this._context.Beers.Add(newBeer);

            await _context.SaveChangesAsync();

            return beer.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> FilterBeersByCountryAsync(string name)
        {
            var beers = await _context.Beers
                 .Include(b => b.Brewery)
                 .Include(s => s.Style)
                 .Where(b => b.Brewery.Country.Name == name).ToListAsync();

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> FilterBeersByStyleAsync(string name)
        {
            var beers = await _context.Beers
                .Include(b => b.Brewery)
                .Include(s => s.Style)
              .Where(b => b.Style.Name == name).ToListAsync();
              

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> SortBeerByNameAsync()
        {
            var beers = await _context.Beers
                .Include(b => b.Brewery)
                .Include(s => s.Style)
                     .Where(b => b.IsDeleted == false)
                     .OrderBy(b => b.Name).ToListAsync();

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> SortBeerByABVAsync()
        {
            var beers = await _context.Beers
                .Include(b => b.Brewery)
                .Include(s => s.Style)
                      .Where(b => b.IsDeleted == false)
                      .OrderBy(b => b.ABV).ToListAsync();

            return beers.GetDTO();
        }
        //Ok
        public async Task<ICollection<BeerDTO>> SortBeerByRatingAsync()
        {
            var beers = await _context.Beers
                .Include(b => b.Brewery)
                .Include(s => s.Style)
                    .Where(b => b.IsDeleted == false)
                    .OrderByDescending(b => b.Rating).ToListAsync();

            return beers.GetDTO();
        }
    }
}