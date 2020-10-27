using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IStyleService
    {
        StyleDTO GetStyle(int id);
        ICollection<StyleDTO> GetAllStyles();
        Task<StyleDTO> CreateStyleAsync(StyleDTO StyleDTO);
        Task<StyleDTO> UpdateStyleAsync(int id, StyleDTO styleDTO);
        Task<StyleDTO> DeleteStyleAsync(int id);
    }
}
