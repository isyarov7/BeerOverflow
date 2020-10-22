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
        Task<BreweryDTO> GetBreweryAsync(int id);
        Task<ICollection<BreweryDTO>> GetAllBreweriesAsync();
        Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO);
        Task<BreweryDTO> UpdateBreweryAsync(int id, BreweryDTO breweryDTO);
        Task<BreweryDTO> DeleteBreweryAsync(int id);
    }
}
