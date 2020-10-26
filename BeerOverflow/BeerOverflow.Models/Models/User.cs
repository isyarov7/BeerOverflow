using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<WishList> Wishlist { get; set; } = new List<WishList>();
    }
}
