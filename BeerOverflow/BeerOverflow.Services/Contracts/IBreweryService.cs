using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IBreweryService
    {
        BreweryDTO GetBrewery(int id);
        IEnumerable<BreweryDTO> GetAllBreweries();
        public void CreateBrewery(BreweryDTO breweryDTO);
        public void UpdateBrewery(int id, BreweryDTO breweryDTO);
        public void DeleteBrewery(BreweryDTO breweryDTO);
    }
}
