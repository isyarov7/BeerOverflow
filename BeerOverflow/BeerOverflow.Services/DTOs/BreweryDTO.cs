using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class BreweryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int CountryId { get; set; }
        public ICollection<Beer> Beers { get; set; } = new List<Beer>();
    }
}
