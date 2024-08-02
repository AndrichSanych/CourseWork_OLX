using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities.Filter;

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
