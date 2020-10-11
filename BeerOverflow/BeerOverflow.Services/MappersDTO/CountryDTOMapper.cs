using BeerOverflow.Models;
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
                Name = item.Name,
                Breweries = (ICollection<Brewery>)(item.Breweries?.GetDTO())
            };
        }
        public static ICollection<CountryDTO> GetDTO(this ICollection<Country> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
