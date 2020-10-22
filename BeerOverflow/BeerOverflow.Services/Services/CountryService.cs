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
            if (_context.Countries.Any(b => b.Name == countryDTO.Name))
            {
                var oldCountry = _context.Countries.Where(b => b.Name == countryDTO.Name).FirstOrDefault();
                _context.Countries.Remove(oldCountry);
            }
            _context.Countries.Add(countryDTO.GetCountry());

            await _context.SaveChangesAsync();

            return countryDTO;
        }

        public async Task<CountryDTO> DeleteCountryAsync(int id)
        {
            var country = await this._context.Countries
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

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

        public async Task<CountryDTO> UpdateCountryAsync(int id, CountryDTO countryDTO)
        {
            var country = await this._context.Countries
                     .FirstOrDefaultAsync(x => x.Id == id);

            country.Name = countryDTO.Name;

            await _context.SaveChangesAsync();

            return country.GetDTO();
        }       
    }
}