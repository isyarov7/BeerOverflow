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
        IEnumerable<BreweryDTO> GetAllBreweries();
        public void CreateBrewery(BreweryDTO breweryDTO);
        public void UpdateBrewery(int id, BreweryDTO breweryDTO);
        public void DeleteBrewery(BreweryDTO breweryDTO);
        Task<BreweryDTO> GetBreweryAsync(int id);
        Task<ICollection<BreweryDTO>> GetAllBreweriesAsync();
        Task<BreweryDTO> CreateBreweryAsync(BreweryDTO breweryDTO);
        Task<BreweryDTO> UpdateBreweryAsync(string oldName, string newName);
        Task<BreweryDTO> DeleteBreweryAsync(string name);
    }
}
