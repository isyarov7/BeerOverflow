using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IBeerService
    {
        BeerDTO GetBeer(int id);
        IEnumerable<BeerDTO> GetAllBeers();
        public void CreateBeer(BeerDTO beerDTO);
        public void UpdateBeer(int id, BeerDTO beerDTO);
        public void DeleteBeer(int id);
    }
}
