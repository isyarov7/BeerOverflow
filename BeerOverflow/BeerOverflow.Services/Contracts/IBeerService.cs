using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBeerService
    {
        BeerDTO GetBeer(string name);
        public void CreateBeer(BeerDTO beerDTO);
        public void UpdateBeer(int id, BeerDTO beerDTO);
        public void DeleteBeer(BeerDTO beerDTO);
        IEnumerable<BeerDTO> GetAllBeers();
        IEnumerable<BeerDTO> FilterBeersByCountry(string name);
        IEnumerable<BeerDTO> FilterBeersByStyle(string name);
        IEnumerable<BeerDTO> SortBeerByName();
        IEnumerable<BeerDTO> SortBeerByABV();
        IEnumerable<BeerDTO> SortBeerByRating();
        Task<ICollection<BeerDTO>> GetAllBeersAsync();
        Task<BeerDTO> GetBeerAsync(int id);
        Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO);
        Task<BeerDTO> UpdateBeerAsync(BeerDTO beerDTO, string name);
        Task<BeerDTO> DeleteBeerAsync(BeerDTO beerDTO);
    }
}
