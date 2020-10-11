using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IStyleService
    {
        StyleDTO GetStyle(int id);
        IEnumerable<StyleDTO> GetAllStyles();
        StyleDTO CreateStyle(StyleDTO styleDTO);
        StyleDTO UpdateStyle(int id, StyleDTO styleDTO);
        public bool DeleteStyle(int id);
    }
}
