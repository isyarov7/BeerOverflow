using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface ICountryService
    {
        CountryDTO GetCountry(string name);
        IEnumerable<CountryDTO> GetAllCountries();
        public void CreateCountry(CountryDTO countryDTO);
        public void UpdateCountry(CountryDTO countryDTO, string name);
        public void DeleteCountry(CountryDTO countryDTO);
        Task<CountryDTO> GetCountryAsync(int id);
        Task<CountryDTO> GetAllCountriesAsync();
        Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO);
        Task<CountryDTO> UpdateCountryAsync(CountryDTO countryDTO, string name);
        Task<CountryDTO> DeleteCountryAsync(CountryDTO countryDTO);
    }
}
