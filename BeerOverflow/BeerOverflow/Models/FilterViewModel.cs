using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class FilterViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<BeerDTO> SearchResults { get; set; }
        public BeerDTO BeerDTO { get; set; }

    }
}
