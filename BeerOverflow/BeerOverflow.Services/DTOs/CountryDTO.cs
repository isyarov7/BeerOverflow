using BeerOverflow.Models.Models;
using System.Collections.Generic;

namespace BeerOverflow.Services.DTO
{
    public class CountryDTO
    {
        public string Name { get; set; }
        public ICollection<Brewery> Breweries { get; internal set; }
    }
}
