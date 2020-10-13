using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOMappers;
using Microsoft.EntityFrameworkCore;
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

            var toRevive = _context.Countries.Where(c => c.Name == country.Name && c.IsDeleted == true).FirstOrDefault();

            if (_context.Countries.Any(c => c.Name == country.Name && c.IsDeleted == true))
            {
                toRevive.IsDeleted = false;
            }
            else if (_context.Countries.Any(c => c.Name == country.Name))
            {
                throw new ArgumentException("Country Already Exists!");
            }
            else
            {
                _context.Countries.Add(country);
            }

            _context.SaveChanges();
        }

        public void DeleteCountry(CountryDTO countryDTO)
        {
            var country = _context.Countries
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Name == countryDTO.Name);

            if (country == null)
            {
                throw new ArgumentNullException();
            }

            country.IsDeleted = true;

            _context.SaveChanges();
        }
        //TODO
        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countries = _context.Countries
                .Where(country => country.IsDeleted == false)
                .Select(country => country.GetDTO()).ToList();

            return countries;
        }
        //TODO
        public CountryDTO GetCountry(int id)
        {
            var country = _context.Countries
                .Where(country => country.IsDeleted == false)
                .FirstOrDefault(country => country.Id == id).GetDTO();

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

        public void UpdateCountry(CountryDTO countryDTO, string newName)
        {
            var country = _context.Countries
               .Where(x => x.IsDeleted == false)
               .FirstOrDefault(x => x.Name == countryDTO.Name);

            if (country == null)
            {
                throw new ArgumentNullException();
            }

            country.Name = newName;

            _context.SaveChanges();
        }
    }
}