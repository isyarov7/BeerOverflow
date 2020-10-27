using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBeerService
    {
        ICollection<BeerDTO> GetAllBeers();
        BeerDTO GetBeer(int id);
        Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO);
        Task<BeerDTO> UpdateBeerAsync(int id, BeerDTO beerDTO);
        Task<BeerDTO> DeleteBeerAsync(int id);
        Task<ICollection<BeerDTO>> FilterBeersByCountryAsync(string name);
        Task<ICollection<BeerDTO>> FilterBeersByStyleAsync(string name);
        Task<ICollection<BeerDTO>> SortBeerByNameAsync();
        Task<ICollection<BeerDTO>> SortBeerByABVAsync();
        Task<ICollection<BeerDTO>> SortBeerByRatingAsync();

    }
}
