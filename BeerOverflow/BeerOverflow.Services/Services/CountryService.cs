using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly BeerOverflowDbContext _context;
        private readonly IMapper _mapper;
        public CountryService(BeerOverflowDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public CountryDTO CreateCountry(CountryDTO country)
        {
            var countryEntity = _mapper.Map<Country>(country);
            this._context.Countries.Add(countryEntity);
            this._context.SaveChanges();
            return country;
        }
    }
}
