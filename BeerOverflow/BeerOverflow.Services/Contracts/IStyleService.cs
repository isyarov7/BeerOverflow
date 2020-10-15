using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IStyleService
    {
        StyleDTO GetReview(int id);
        IEnumerable<StyleDTO> GetAllStyles();
        public void CreateStyle(StyleDTO styleDTO);
        public void UpdateStyle(int id, StyleDTO styleDTO);
        public void DeleteStyle(StyleDTO styleDTO);
        Task<StyleDTO> GetStyleAsync(int id);
        Task<StyleDTO> GetAllStylesAsync();
        Task<StyleDTO> CreateStyleAsync(StyleDTO StyleDTO);
        Task<StyleDTO> UpdateStyleAsync(StyleDTO StyleDTO, string name);
        Task<StyleDTO> DeleteStyleAsync(StyleDTO StyleDTO);
    }
}
