using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapper
{
    public  class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserModel, User>()
                .ForMember(x => x.UserName, opts => opts.MapFrom(s => s.Email));
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
