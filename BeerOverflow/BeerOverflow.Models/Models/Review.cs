using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerOverflow.Models.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public bool IsDeleted { get; set; }
    }
}
