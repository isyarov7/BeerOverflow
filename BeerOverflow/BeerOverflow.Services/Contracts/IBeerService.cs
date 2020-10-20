using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBeerService
    {
        Task<ICollection<BeerDTO>> GetAllBeersAsync();
        Task<BeerDTO> GetBeerAsync(int id);
        Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO);
        Task<BeerDTO> UpdateBeerAsync(string beerDTO, string name);
        Task<BeerDTO> DeleteBeerAsync(string name);
        Task<ICollection<BeerDTO>> FilterBeersByCountryAsync(string name);
        Task<ICollection<BeerDTO>> FilterBeersByStyleAsync(string name);
        Task<ICollection<BeerDTO>> SortBeerByNameAsync();
        Task<ICollection<BeerDTO>> SortBeerByABVAsync();
        Task<ICollection<BeerDTO>> SortBeerByRatingAsync();

    }
}
