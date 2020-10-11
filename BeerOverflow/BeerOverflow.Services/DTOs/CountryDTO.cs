using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTO
{
    public class CountryDTO
    {
        public string Name { get; set; }
        public ICollection<Brewery> Breweries { get; internal set; }
    }
}
