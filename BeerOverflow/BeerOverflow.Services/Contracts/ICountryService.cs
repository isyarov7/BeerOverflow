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
        Task<CountryDTO> UpdateCountryAsync(string oldName, string newName);
        Task<CountryDTO> DeleteCountryAsync(string name);
    }
}
