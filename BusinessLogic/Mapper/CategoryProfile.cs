using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;

namespace BusinessLogic.Mapper
{
    public class CategoryProfile :Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}