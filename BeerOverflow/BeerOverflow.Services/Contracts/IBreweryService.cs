using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBreweryService
    {
        BreweryDTO GetBrewery(int id);
        ICollection<BreweryDTO> GetAllBreweries();
        Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO);
        Task<BreweryDTO> UpdateBreweryAsync(int id, BreweryDTO breweryDTO);
        Task<BreweryDTO> DeleteBreweryAsync(int id);
    }
}
