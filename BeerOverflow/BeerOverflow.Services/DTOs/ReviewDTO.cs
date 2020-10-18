using BeerOverflow.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class ReviewDTO
    {
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int BeerId { get; set; }
    }
}
