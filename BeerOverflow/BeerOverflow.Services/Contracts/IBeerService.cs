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
        BeerDTO CreateBeer(BeerDTO beerDTO);
        BeerDTO UpdateBeer(int id, BeerDTO beerDTO);
        public bool DeleteBeer(int id);
    }
}
