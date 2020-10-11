using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.DTOMappers
{
    public static class BreweryDTOMapper
    {
        public static BreweryDTO GetDTO(this Brewery item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new BreweryDTO
            {
                Name = item.Name,
                CountryId = item.CountryId,
                Beers= (ICollection<Beer>)(item.Beers?.GetDTO()),
            };
        }
        public static ICollection<BreweryDTO> GetDTO(this ICollection<Brewery> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
