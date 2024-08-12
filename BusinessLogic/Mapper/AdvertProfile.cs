using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Models.AdvertModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace BusinessLogic.Mapper
{
    public class SetAdvertIsFavorite : IMappingAction<Advert, AdvertDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SetAdvertIsFavorite(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void Process(Advert source, AdvertDto destination, ResolutionContext context)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            destination.isFavorite = source.UserFavouriteAdverts.Any(x => x.UserId == userId);
        }
    }
    internal class AdvertProfile:Profile
    {
        public AdvertProfile() 
        {
             _ = CreateMap<Advert, AdvertDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.CityName, opt => opt.MapFrom(x => x.City.Name))
                .ForMember(x => x.FirstImage, opt => opt.MapFrom(x => x.Images.FirstOrDefault(x => x.Priority == 0).Name ?? "Error first image"))
                .ForMember(x=>x.AreaName,opt=>opt.MapFrom(x=>x.City.Area.Name))
                .ForMember(x => x.AreaId, opt => opt.MapFrom(x => x.City.Area.Id))
                .AfterMap<SetAdvertIsFavorite>();
                
            CreateMap<AdvertDto, Advert>();
            CreateMap<AdvertCreationModel, Advert>();
            
        }
    }
}
