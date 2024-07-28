using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Models.AdvertModels;


namespace BusinessLogic.Mapper
{
    internal class AdvertProfile:Profile
    {
        public AdvertProfile() 
        {
            _ = CreateMap<Advert, AdvertDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.CityName, opt => opt.MapFrom(x => x.City.Name))
                .ForMember(x => x.FirstImage, opt => opt.MapFrom(x => x.Images.FirstOrDefault(x => x.Priority == 0).Name ?? "Error first image"));
                
            CreateMap<AdvertDto, Advert>();
            CreateMap<AdvertCreationModel, Advert>();
            CreateMap<AdvertUpdateModel, Advert>()
                .ForMember(x=>x.Images,opt=>opt.Ignore());
        }
    }
}
