using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.DTOMappers
{
    public static class BeerDTOMapper
    {
        public static BeerDTO GetDTO(this Beer item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new BeerDTO
            {
                Name = item.Name,
                Description = item.Description,
                ABV = item.ABV,
                Rating = item.Rating,
                StyleId = item.StyleId,
                BreweryId = item.BreweryId,
                Milliliters = item.Milliliters
            };
        }
        public static ICollection<BeerDTO> GetDTO(this ICollection<Beer> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
