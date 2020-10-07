using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface ICountryService
    {
        CountryDTO CreateCountry(CountryDTO country);
    }
}
