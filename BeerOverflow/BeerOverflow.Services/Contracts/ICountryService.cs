using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface ICountryService
    {
        CountryDTO GetCountry(int id);
        IEnumerable<CountryDTO> GetAllCountries();
        public void CreateCountry(CountryDTO countryDTO);
        public void UpdateCountry(CountryDTO countryDTO, string name);
        public void DeleteCountry(CountryDTO countryDTO);

        //Тези неща ги слагаш навсякъде в другите interface-и, след което 
        //разписваш сървизите като CountryService 
        Task<CountryDTO> GetCountryAsync(int id);
        Task<CountryDTO> GetAllCountriesAsync();
        Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO);
        Task<CountryDTO> UpdateCountryAsync(CountryDTO countryDTO, string name);
        Task<CountryDTO> DeleteCountryAsync(CountryDTO countryDTO);
    }
}
