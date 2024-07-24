using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapper
{
    internal class AreaProfile : Profile
    {
        public AreaProfile() 
        {
            CreateMap<Area, AreaDto>().ReverseMap();
        }
    }
}
