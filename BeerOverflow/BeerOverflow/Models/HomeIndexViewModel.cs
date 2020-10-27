using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class HomeIndexViewModel
    {
        public List<BeerViewModel> TopRatedBeers { get; set; }
        public List<BreweryViewModel> TopRatedBreweries { get; set; }
    }
}
