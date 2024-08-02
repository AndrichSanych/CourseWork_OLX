using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities.Filter;


namespace BusinessLogic.Mapper
{
    internal class FilterProfile:Profile
    {
        public FilterProfile() 
        {
            CreateMap<Filter, FilterDto>()
                .ForMember(x=>x.Values,opt=>
                opt.MapFrom(z=>z.Values.Select(y=> new FilterValueDto {Id=y.Id,FilterId = y.FilterId,Value=y.Value }).ToArray()));
        }
    }
}
