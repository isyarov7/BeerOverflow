using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Brewery> Breweries { get; set; } = new List<Brewery>();
    }
}
