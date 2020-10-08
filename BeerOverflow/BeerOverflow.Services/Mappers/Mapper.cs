using AutoMapper;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            //CreateMap<Beer, BeerDTO>();
            //CreateMap<Brewery, BreweryDTO>();
            //CreateMap<Style, StyleDTO>();
            //CreateMap<Review, ReviewDTO>();
        }
    }
}
