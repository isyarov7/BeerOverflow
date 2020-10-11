using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using System.Collections.Generic;

namespace BeerOverflow.Services.Services
{
    public class BeerService : IBeerService
    {
        private readonly BeerOverflowDbContext _context;
        public BeerService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public BeerDTO CreateBeer(BeerDTO beerDTO)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteBeer(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BeerDTO> GetAllBeers()
        {
            throw new System.NotImplementedException();
        }

        public BeerDTO GetBeer(int id)
        {
            throw new System.NotImplementedException();
        }

        public BeerDTO UpdateBeer(int id, BeerDTO beerDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
