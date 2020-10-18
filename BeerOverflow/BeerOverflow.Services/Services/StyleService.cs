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

        public void CreateStyle(StyleDTO styleDTO)
        {

            var style = new Style
            {
                Name = styleDTO.Name,
                Description = styleDTO.Description
            };

            var alreadyCreated = _context.Styles.Where(b => b.Name == style.Name).FirstOrDefault();

            if (alreadyCreated.IsDeleted == true)
            {
                alreadyCreated.IsDeleted = false;
            }
            else if (alreadyCreated.IsDeleted == false)
            {
                alreadyCreated.IsDeleted = false;
            }
            else
            {
                _context.Styles.Add(style);
            }

            _context.Styles.Add(style);

            _context.SaveChanges();
        }

        public void DeleteStyle(StyleDTO styleDTO)
        {
            var style = _context.Styles
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Name == styleDTO.Name);

            if (style == null)
            {
                throw new ArgumentNullException();
            }

            style.IsDeleted = true;

            _context.SaveChanges();
        }

        public IEnumerable<StyleDTO> GetAllStyles()
        {
            var styles = _context.Styles
               .Where(st => st.IsDeleted == false)
               .Include(st => new StyleDTO
               {
                   Name = st.Name,
               })
              .ToList()
              .GetDTO();

            return styles;
        }

        public StyleDTO GetStyle(string name)
        {
            var style = _context.Styles
                .Where(st => st.IsDeleted == false)
                .FirstOrDefault(st => st.Name == name).GetDTO();

            if (style == null)
            {
                throw new ArgumentNullException();
            }

            var styleDTO = new StyleDTO
            {
                Name = style.Name,
            };

            return styleDTO;
        }

        public void UpdateStyle(StyleDTO styleDTO, string name)
        {
            var style = _context.Styles.Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Name == name);

            if (style == null)
            {
                throw new ArgumentNullException();
            }

            style.Name = styleDTO.Name;

            _context.SaveChanges();
        }
        public async Task<StyleDTO> CreateStyleAsync(StyleDTO styleDTO)
        {
            if (!_context.Styles.Any(b => b.Name == styleDTO.Name))
            {
                _context.Styles.Add(styleDTO.GetStyle());
            }
            else
            {
                var style = _context.Styles.Where(b => b.Name == styleDTO.Name).FirstOrDefault();
                style.IsDeleted = false;
            }


            await _context.SaveChangesAsync();

            return styleDTO;
        }

        public async Task<StyleDTO> DeleteStyleAsync(string name)
        {
            var style = await Task.Run(() => this._context.Styles
               .FirstOrDefaultAsync(x => x.Name == name && x.IsDeleted == false));

            style.IsDeleted = true;

            await _context.SaveChangesAsync();

            return style.GetDTO();
        }
        public async Task<ICollection<StyleDTO>> GetAllStylesAsync()
        {
            var styles = await Task.Run(() => this._context.Styles
            .Where(b => b.IsDeleted == false)
            .Select(b => b.GetDTO())
            .ToListAsync());

            return styles;
        }
        public async Task<StyleDTO> GetStyleAsync(int id)
        {

            var styles = await Task.Run(() => this._context.Styles
                 .FirstOrDefaultAsync(r => r.Id == id));

            return styles.GetDTO();
        }

        public async Task<StyleDTO> UpdateStyleAsync(string oldName, string newName)
        {
            var styles = await Task.Run(() => this._context.Styles
                   .FirstOrDefaultAsync(x => x.Name == oldName));

            styles.Name = newName;

            await _context.SaveChangesAsync();

            return styles.GetDTO();
        }
    }
}