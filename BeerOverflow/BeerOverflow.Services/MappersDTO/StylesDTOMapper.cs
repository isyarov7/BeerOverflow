using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.DTOMappers
{
    public static class StyleDTOMapper
    {
        public static StyleDTO GetDTO(this Style item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new StyleDTO
            {
                Beers = item.Beers,
                Description = item.Description,
                Name = item.Name
            };
        }

        public static Style GetStyle(this StyleDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new Style
            {
                Beers = item.Beers,
                Description = item.Description,
                Name = item.Name
            };
        }

        public static ICollection<StyleDTO> GetDTO(this ICollection<Style> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}