using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly BeerOverflowDbContext _context;
        public CountryService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public void CreateCountry(CountryDTO countryDTO)
        {
            if (countryDTO == null)
            {
                throw new ArgumentNullException();
            }

            var country = new Country
            {
                Name = countryDTO.Name,
            };

            if (_context.Countries.Any(b => b.Name == country.Name))
            {
                throw new ArgumentException("Country with such name already exists!");
            }

            _context.Countries.Add(country);

            _context.SaveChanges();
        }

        public void DeleteCountry(int id)
        {
            var country = _context.Countries
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (country == null)
            {
                throw new ArgumentNullException();
            }

            country.IsDeleted = true;

            _context.SaveChanges();
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countries = _context.Countries
                .Where(country => country.IsDeleted == false)
                .Select(country => new CountryDTO
                {
                    Name = country.Name,
                })
               .ToList();

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
                Name = country.Name,
            };

            return countryDTO;
        }

        public void UpdateCountry(int id, CountryDTO countryDTO)
        {
            var country = _context.Countries.Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (country == null)
            {
                throw new ArgumentNullException();
            }

            country.Name = countryDTO.Name;

            _context.SaveChanges();
        }
    }
}