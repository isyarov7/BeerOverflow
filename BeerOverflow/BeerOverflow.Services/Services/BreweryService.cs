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
    public class BreweryService : IBreweryService
    {
        private readonly BeerOverflowDbContext _context;
        private readonly IMapper _mapper;
        public BreweryService(BeerOverflowDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public BreweryDTO CreateBrewery(BreweryDTO brewery)
        {
            var breweryEntity = _mapper.Map<Brewery>(brewery);
            this._context.Breweries.Add(breweryEntity);
            this._context.SaveChanges();
            return brewery;
        }
    }
}
