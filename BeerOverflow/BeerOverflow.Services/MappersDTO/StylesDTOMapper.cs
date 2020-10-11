using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;

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
                Id=item.Id,
                Name = item.Name,
            };
        }
    }
}