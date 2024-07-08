﻿using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Models.AdvertModels;


namespace BusinessLogic.Mapper
{
    internal class AdvertProfile:Profile
    {
        public AdvertProfile() 
        {
            CreateMap<Advert, AdvertDto>()
                .ForMember(x=>x.CategoryName,opt=>opt.MapFrom(x=>x.Category.Name))
                .ForMember(x=>x.CityName,opt=>opt.MapFrom(x=>x.City.Name));
            CreateMap<AdvertDto, Advert>();
            CreateMap<AdvertCreationModel, Advert>();
        }
    }
}
