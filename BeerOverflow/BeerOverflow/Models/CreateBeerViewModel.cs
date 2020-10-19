using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class CreateBeerViewModel
    {
        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }
        public string ABV { get; set; }
        public int Brewery { get; set; }
        public double Rating { get; set; }
        public string Milliliters { get; set; }
        public string Description { get; set; }
        public int Style { get; set; }
    }
}
