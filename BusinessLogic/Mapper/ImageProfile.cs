using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;


namespace BusinessLogic.Mapper
{
    internal class ImageProfile:Profile
    {
        public ImageProfile() 
        {
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
