using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;

namespace BusinessLogic.Mapper
{
    public class CitiesProfile :Profile
    {
        public CitiesProfile() 
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
