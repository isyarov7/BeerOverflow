using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IStyleService
    {
        Task<StyleDTO> GetStyleAsync(int id);
        Task<ICollection<StyleDTO>> GetAllStylesAsync();
        Task<StyleDTO> CreateStyleAsync(StyleDTO StyleDTO);
        Task<StyleDTO> UpdateStyleAsync(int id, StyleDTO styleDTO);
        Task<StyleDTO> DeleteStyleAsync(int id);
    }
}
