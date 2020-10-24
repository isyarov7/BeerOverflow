using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<WishList> Wishlist { get; set; } = new List<WishList>();
    }
}
