using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class BeerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ABV { get; set; }
        public double Rating { get; set; }
        public int BreweryId { get; set; }
        public string Milliliters { get; set; }
        public string Description { get; set; }
        public Brewery Brewery { get; set; }
        public Style Style { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
    }
}
