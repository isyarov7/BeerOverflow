using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerOverflow.Models.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Beer> Beers { get; set; } = new List<Beer>();
    }
}
