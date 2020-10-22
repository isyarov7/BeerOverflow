using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int BeerId { get; set; }
    }
}
