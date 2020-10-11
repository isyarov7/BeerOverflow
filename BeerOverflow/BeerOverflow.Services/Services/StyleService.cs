using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Services
{
    public class StyleService : IStyleService
    {
        private readonly BeerOverflowDbContext _context;
        public StyleService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public StyleDTO CreateStyle(StyleDTO styleDTO)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStyle(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StyleDTO> GetAllStyles()
        {
            throw new NotImplementedException();
        }

        public StyleDTO GetStyle(int id)
        {
            throw new NotImplementedException();
        }

        public StyleDTO UpdateStyle(int id, StyleDTO styleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
