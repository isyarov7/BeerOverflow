using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly BeerOverflowDbContext _context;
        public CountryService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public CountryDTO CreateCountry(CountryDTO countryDTO)
        {
            var country = new Country
            {
                Id = countryDTO.Id,
                Name = countryDTO.Name,
            };

            _context.Countries.Add(country);

            return countryDTO;
        }

        public bool DeleteCountry(int id)
        {
            try
            {
                var beer = _context.Countries
                    .Where(x => x.IsDeleted == false)
                    .FirstOrDefault(x => x.Id == id);

                beer.IsDeleted = true;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countries = _context.Countries
                .Where(country => country.IsDeleted == false)
                .Select(country => new CountryDTO
                {
                    Id = country.Id,
                    Name = country.Name,
                });

            return countries;
        }

        public CountryDTO GetCountry(int id)
        {
            var country = _context.Countries
                .Where(country => country.IsDeleted == false)
                .FirstOrDefault(country => country.Id == id);

            if (country == null)
            {
                throw new ArgumentNullException();
            }

            var countryDTO = new CountryDTO
            {
                Id = country.Id,
                Name = country.Name,
            };

            return countryDTO;
        }

        public CountryDTO UpdateCountry(int id, CountryDTO countryDTO)
        {
            var country = _context.Countries.Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (country == null)
            {
                throw new ArgumentNullException();
            }

            country.Name = countryDTO.Name;

            return countryDTO;
        }
    }
}