﻿using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;

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
    }
}