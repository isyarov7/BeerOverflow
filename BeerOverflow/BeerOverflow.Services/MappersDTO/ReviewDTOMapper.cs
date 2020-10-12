using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflow.Services.MappersDTO
{
    public static class ReviewDTOMapper
    {
        public static ReviewDTO GetDTO(this Review item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new ReviewDTO
            {
                Content = item.Content,
                BeerId = item.BeerId
            };
        }
        public static ICollection<ReviewDTO> GetDTO(this ICollection<Review> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
