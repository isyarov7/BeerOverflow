using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class StyleDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Beer> Beers { get; set; } = new List<Beer>();    
    }
}
