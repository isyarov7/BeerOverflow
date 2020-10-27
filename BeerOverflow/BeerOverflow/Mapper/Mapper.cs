using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.DTO;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.NewFolder
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<BeerDTO, BeerViewModel>().ReverseMap();
            CreateMap<CountryDTO, CountryViewModel>().ReverseMap();
            CreateMap<BreweryDTO, BreweryViewModel>().ReverseMap();
            CreateMap<ReviewDTO, ReviewViewModel>().ReverseMap();
            CreateMap<StyleDTO, StyleViewModel>().ReverseMap();
            CreateMap<BeerDTO, HomeIndexViewModel>().ReverseMap();
            CreateMap<BeerViewModel, HomeIndexViewModel>().ReverseMap();
            CreateMap<BreweryViewModel, HomeIndexViewModel>().ReverseMap();
        }
    }
}
