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
                BeerId = item.BeerId,
                IsDeleted = item.IsDeleted,
                Content = item.Content
            };
        }

        public static Review GetReview(this ReviewDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new Review
            {
                BeerId = item.BeerId,
                IsDeleted = item.IsDeleted,
                Content = item.Content
            };
        }

        public static ICollection<ReviewDTO> GetDTO(this ICollection<Review> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
