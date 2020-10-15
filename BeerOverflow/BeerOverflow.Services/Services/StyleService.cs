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
            if (styleDTO == null)
            {
                throw new ArgumentNullException();
            }

            var style = (await this._context.Styles
                .FirstOrDefaultAsync(c => c.Name == styleDTO.Name));

            var toRevive = _context.Styles.Where(c => c.Name == style.Name && c.IsDeleted == true)
                .FirstOrDefault();

            if (_context.Styles.Any(c => c.Name == style.Name && c.IsDeleted == true))
            {
                toRevive.IsDeleted = false;
            }
            else if (_context.Styles.Any(c => c.Name == style.Name && c.IsDeleted == false))
            {
                toRevive.IsDeleted = false;
            }
            else
            {
                _context.Styles.Add(style);
            }

            _context.SaveChanges();

            return style.GetDTO();
        }

        public async Task<StyleDTO> DeleteStyleAsync(StyleDTO styleDTO)
        {
            var style = (await this._context.Styles
                .FirstOrDefaultAsync(x => x.Name == styleDTO.Name && x.IsDeleted == false));

            if (style == null)
            {
                throw new ArgumentNullException();
            }

            style.IsDeleted = true;

            _context.SaveChanges();

            return style.GetDTO();
        }
        public async Task<StyleDTO> GetAllStylesAsync()
        {
            var styles = (await this._context.Styles
                .FirstOrDefaultAsync(style => style.IsDeleted == false)).GetDTO();

            return styles;
        }
        public async Task<StyleDTO> GetStyleAsync(int id)
        {
            var style = (await this._context.Styles
                .FirstOrDefaultAsync(style => style.IsDeleted == false)).GetDTO();

            if (style == null)
            {
                throw new ArgumentException();
            }

            return style;
        }

        public async Task<StyleDTO> UpdateStyleAsync(StyleDTO styleDTO, string newName)
        {
            var style = (await this._context.Styles
               .FirstOrDefaultAsync(x => x.Name == styleDTO.Name)).GetDTO();

            if (style == null)
            {
                throw new ArgumentNullException();
            }

            style.Name = newName;

            _context.SaveChanges();

            return style;
        }
    }
}
