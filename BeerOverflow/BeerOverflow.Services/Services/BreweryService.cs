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
        public BreweryService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public BreweryDTO CreateBrewery(BreweryDTO breweryDTO)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBrewery(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BreweryDTO> GetAllBreweries()
        {
            throw new NotImplementedException();
        }

        public BreweryDTO GetBrewery(int id)
        {
            throw new NotImplementedException();
        }

        public BreweryDTO UpdateBrewery(int id, BreweryDTO breweryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
