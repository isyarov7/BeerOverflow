using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.DTOMappers
{
    public static class CountryDTOMapper
    {
        public static CountryDTO GetDTO(this Country item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new CountryDTO
            {
                Id = item.Id,
                Name = item.Name,
                Breweries = item.Breweries,
            };
        }

        public static Country GetCountry(this CountryDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new Country
            {
                Id = item.Id,
                Name = item.Name,
                Breweries = item.Breweries,
            };
        }
        public static ICollection<CountryDTO> GetDTO(this ICollection<Country> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
