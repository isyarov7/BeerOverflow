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
        private readonly IMapper _mapper;
        public StyleService(BeerOverflowDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public StyleDTO CreateStyle(StyleDTO style)
        {
            var styleEntity = _mapper.Map<Style>(style);
            this._context.Styles.Add(styleEntity);
            this._context.SaveChanges();
            return style;
        }
    }
}
