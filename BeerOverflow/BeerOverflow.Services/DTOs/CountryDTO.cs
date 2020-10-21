using BeerOverflow.Models.Models;
using System.Collections.Generic;

namespace BeerOverflow.Services.DTO
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Brewery> Breweries { get; set; } = new List<Brewery>();
    }
}
