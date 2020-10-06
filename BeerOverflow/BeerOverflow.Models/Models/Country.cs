using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<Brewery> Breweries { get; set; } = new List<Brewery>();

    }
}
