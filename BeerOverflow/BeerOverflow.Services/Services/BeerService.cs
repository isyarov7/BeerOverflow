using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Services
{
    public class BeerService : IBeerService
    {
        private readonly BeerOverflowDbContext _context;
        private readonly IMapper _mapper;
        public BeerService(BeerOverflowDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public BeerDTO CreateBeer(BeerDTO beer)
        {
            var beerEntity = _mapper.Map<Beer>(beer);
            this._context.Beers.Add(beerEntity);
            this._context.SaveChanges();
            return beer;
        }
    }
}
