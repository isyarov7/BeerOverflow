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
                Id = item.Id,
                Name = item.Name,
                CountryId = item.CountryId,
                IsDeleted = item.IsDeleted,
                Beers = item.Beers
            };
        }

        public static Brewery GetBrewery(this BreweryDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new Brewery
            {
                Id = item.Id,
                Name = item.Name,
                CountryId = item.CountryId,
                IsDeleted = item.IsDeleted,
                Beers = item.Beers
            };

        }

        public static ICollection<BreweryDTO> GetDTO(this ICollection<Brewery> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
