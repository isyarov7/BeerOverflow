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
        public string Description { get; set; }
        public string Milliliters { get; set; }
        public double Rating { get; set; }
        public string ABV { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
        public int StyleId { get; set; }
        public Style Style { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
