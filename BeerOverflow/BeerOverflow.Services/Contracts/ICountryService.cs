using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface ICountryService
    {
        Task<CountryDTO> GetCountryAsync(int id);
        Task<ICollection<CountryDTO>> GetAllCountriesAsync();
        Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO);
        Task<CountryDTO> UpdateCountryAsync(int id, CountryDTO countryDTO);
        Task<CountryDTO> DeleteCountryAsync(int id);
    }
}
