using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class BeerViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Milliliters { get; set; }
        public double Rating { get; set; }
        public string ABV { get; set; }
        public string ImageUrl { get; set; }
        public int BreweryId { get; set; }
        public int StyleId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
