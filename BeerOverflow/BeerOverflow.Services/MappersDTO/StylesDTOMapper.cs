using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.DTOMappers
{
    public static class StylesDTOMapper
    {
        public static StyleDTO GetDTO(this Style item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new StyleDTO
            {
                Name = item.Name,
                Description = item.Description,
                Beers = (ICollection<Beer>)(item.Beers?.GetDTO())
            };
        }
        public static ICollection<StyleDTO> GetDTO(this ICollection<Style> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}