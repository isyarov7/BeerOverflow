using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IStyleService
    {
        StyleDTO GetReview(int id);
        IEnumerable<StyleDTO> GetAllStyles();
        public void CreateStyle(StyleDTO styleDTO);
        public void UpdateStyle(int id, StyleDTO styleDTO);
        public void DeleteStyle(int id);
    }
}
