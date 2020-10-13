using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface ICountryService
    {
        CountryDTO GetCountry(int id);
        IEnumerable<CountryDTO> GetAllCountries();
        public void CreateCountry(CountryDTO countryDTO);
        public void UpdateCountry(CountryDTO countryDTO, string name);
        public void DeleteCountry(CountryDTO countryDTO);
    }
}
