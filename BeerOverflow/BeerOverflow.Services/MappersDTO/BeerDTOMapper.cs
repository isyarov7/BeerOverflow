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
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ABV = item.ABV,
                Rating = item.Rating,
                IsDeleted = item.IsDeleted,
                Brewery = item.Brewery,
                Milliliters = item.Milliliters,
                Reviews = item.Reviews,
                BreweryId = item.BreweryId,
                StyleId = item.StyleId,
                Style = item.Style
            };
        }
        public static Beer GetBeer(this BeerDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new Beer
            {
                Id = item.Id,
                Name = item.Name,
                Brewery = item.Brewery,
                Description = item.Description,
                ABV = item.ABV,
                Rating = item.Rating,
                IsDeleted = item.IsDeleted,
                Milliliters = item.Milliliters,
                Reviews = item.Reviews,
                BreweryId = item.BreweryId,
                StyleId = item.StyleId,
                Style = item.Style
            };
        }
        public static ICollection<BeerDTO> GetDTO(this ICollection<Beer> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
