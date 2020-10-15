using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IBeerService
    {
        BeerDTO GetBeer(int id);
        public void CreateBeer(BeerDTO beerDTO);
        public void UpdateBeer(int id, BeerDTO beerDTO);
        public void DeleteBeer(BeerDTO beerDTO);
        IEnumerable<BeerDTO> GetAllBeers();
        IEnumerable<BeerDTO> FilterBeersByCountry();
        IEnumerable<BeerDTO> FilterBeersByStyle();
        IEnumerable<BeerDTO> SortBeerByName();
        IEnumerable<BeerDTO> SortBeerByABV();
        IEnumerable<BeerDTO> SortBeerByRating();
        Task<BeerDTO> GetBeerAsync(int id);
        Task<BeerDTO> GetAllBeersAsync();
        Task<BeerDTO> CreateBeerAsync(BeerDTO beerDTO);
        Task<BeerDTO> UpdateBeerAsync(BeerDTO beerDTO, string name);
        Task<BeerDTO> DeleteBeerAsync(BeerDTO beerDTO);

    }
}
