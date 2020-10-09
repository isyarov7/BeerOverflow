using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models.Models
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string ABV { get; set; }
        public int Rating { get; set; }
        public int BreweryId { get; set; }
        public string ImageUrl { get; set; }
        public string Milliliters { get; set; }
        public Brewery Brewery { get; set; }
        public int StyleId { get; set; }
        public Style Style { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
    }
}
