using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOMappers;
using BeerOverflow.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Services
{
    public class StyleService : IStyleService
    {
        private readonly BeerOverflowDbContext _context;
        public StyleService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public async Task<StyleDTO> CreateStyleAsync(StyleDTO styleDTO)
        {
            if (_context.Styles.Any(b => b.Name == styleDTO.Name))
            {
                var oldStyle = _context.Styles.Where(b => b.Name == styleDTO.Name).FirstOrDefault();
                _context.Styles.Remove(oldStyle);
            }
                _context.Styles.Add(styleDTO.GetStyle());


            await _context.SaveChangesAsync();

            return styleDTO;
        }

        public async Task<StyleDTO> DeleteStyleAsync(int id)
        {
            var style = await this._context.Styles
               .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            style.IsDeleted = true;

            await _context.SaveChangesAsync();

            return style.GetDTO();
        }
        public ICollection<StyleDTO> GetAllStyles()
        {
            var styles =  this._context.Styles
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO())
            .ToList();

            return styles;
        }
        public StyleDTO GetStyle(int id)
        {

            var styles = this._context.Styles
                 .FirstOrDefault(r => r.Id == id);

            return styles.GetDTO();
        }

        public async Task<StyleDTO> UpdateStyleAsync(int id, StyleDTO styleDTO)
        {
            var styles = await this._context.Styles
                   .FirstOrDefaultAsync(x => x.Id == id);

            styles.Name = styleDTO.Name;

            await _context.SaveChangesAsync();

            return styles.GetDTO();
        }
    }
}