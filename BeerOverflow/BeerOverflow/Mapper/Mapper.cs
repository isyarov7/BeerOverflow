using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CountryDTO, CountryViewModel>().ReverseMap();
            CreateMap<CountryDTO, NewCountryViewModel>().ReverseMap();
            CreateMap<BeerDTO, CreateBeerViewModel>().ReverseMap();
            CreateMap<Brewery, BreweryDTO>();
            CreateMap<Style, StyleDTO>();
            CreateMap<Review, ReviewDTO>();
        }
    }
}
