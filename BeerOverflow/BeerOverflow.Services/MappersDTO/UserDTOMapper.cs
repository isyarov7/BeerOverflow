using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.MappersDTO
{
    public static class UserDTOMapper
    {
        public static UserDTO GetDTO(this User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new UserDTO
            {
                CreatedOn = item.CreatedOn,
                IsDeleted = item.IsDeleted,
                IsAdmin = item.IsAdmin,
                IsBanned = item.IsBanned,
                Wishlist = item.Wishlist,

            };
        }
        public static User GetUser(this UserDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            return new User
            {
                CreatedOn = item.CreatedOn,
                IsDeleted = item.IsDeleted,
                IsAdmin = item.IsAdmin,
                IsBanned = item.IsBanned,
                Wishlist = item.Wishlist,

            };
        }



        public static ICollection<UserDTO> GetDTO(this ICollection<User> items)
        {
            return items.Select(GetDTO).ToList();
        }
    }
}
