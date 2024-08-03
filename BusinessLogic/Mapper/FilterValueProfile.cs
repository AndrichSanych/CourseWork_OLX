using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapper
{
    internal class FilterValueProfile : Profile
    {
        public FilterValueProfile()
        {
            CreateMap<FilterValue, FilterValueDto>()
                .ForMember(x=>x.FilterName,opt=>
                opt.MapFrom(z=>z.Filter.Name));
        }
    }
}
