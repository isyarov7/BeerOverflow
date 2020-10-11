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
        CountryDTO CreateCountry(CountryDTO countryDTO);
        CountryDTO UpdateCountry(int id, CountryDTO countryDTO);
        public bool DeleteCountry(int id);
    }
}
