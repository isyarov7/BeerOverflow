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
        public void UpdateCountry(int id, CountryDTO countryDTO);
        public void DeleteCountry(int id);
    }
}
