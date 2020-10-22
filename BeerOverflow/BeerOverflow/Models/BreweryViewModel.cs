using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class BreweryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public ICollection<Beer> Beers { get; set; } = new List<Beer>();
    }
}
