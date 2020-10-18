using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOMappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly BeerOverflowDbContext _context;
        public CountryService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public async Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO)
        {
            if (!_context.Countries.Any(b => b.Name == countryDTO.Name))
            {
                _context.Countries.Add(countryDTO.GetCountry());
            }
            else
            {
                var country = _context.Countries.Where(b => b.Name == countryDTO.Name).FirstOrDefault();
                country.IsDeleted = false;
            }


            await _context.SaveChangesAsync();

            return countryDTO;
        }

        public async Task<CountryDTO> DeleteCountryAsync(string name)
        {
            var country = await Task.Run(() => this._context.Countries
                    .FirstOrDefaultAsync(x => x.Name == name && x.IsDeleted == false));

            country.IsDeleted = true;

            await _context.SaveChangesAsync();

            return country.GetDTO();
        }

        public async Task<ICollection<CountryDTO>> GetAllCountriesAsync()
        {
            var countries = await Task.Run(() => this._context.Countries
            .Where(c => c.IsDeleted == false)
             .Select(b => b.GetDTO())
               .ToListAsync());

            return countries;
        }

        public async Task<CountryDTO> GetCountryAsync(int id)
        {
            var country = await Task.Run(() => this._context.Countries
                  .FirstOrDefaultAsync(country => country.Id == id));

            return country.GetDTO();
        }

        public async Task<CountryDTO> UpdateCountryAsync(string oldName, string newName)
        {
            var country = await Task.Run(() => this._context.Countries
                     .FirstOrDefaultAsync(x => x.Name == oldName));

            country.Name = newName;

            await _context.SaveChangesAsync();

            return country.GetDTO();
        }

        public void CreateCountry(CountryDTO countryDTO)
        {
            if (countryDTO == null)
            {
                throw new ArgumentNullException();
            }

            var country = new Country
            {
                Name = countryDTO.Name
            };

            var toRevive = _context.Countries.Where(c => c.Name == country.Name && c.IsDeleted == true).FirstOrDefault();

            if (_context.Countries.Any(c => c.Name == country.Name && c.IsDeleted == true))
            {
                toRevive.IsDeleted = false;
            }
            else if (_context.Countries.Any(c => c.Name == country.Name && c.IsDeleted == false))
            {
                toRevive.IsDeleted = false;
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
        public IEnumerable<CountryDTO> GetAllCountries()
        {
            var countries = _context.Countries
                .Where(country => country.IsDeleted == false)
                .Select(country => country.GetDTO()).ToList();

            return countries;
        }
        public CountryDTO GetCountry(string name)
        {
            var country = _context.Countries
                .Where(country => country.IsDeleted == false)
                .FirstOrDefault(country => country.Name == name).GetDTO();

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