using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using System.Net;

namespace BusinessLogic.Services
{
    internal class CitiesService : ICitiesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<City> cities;

        public CitiesService(IMapper mapper,IRepository<City> cities)
        {
            this.mapper = mapper;
            this.cities = cities;
        }
        public async Task<IEnumerable<CityDto>> GetAllAsync() => mapper.Map<IEnumerable<CityDto>>(await cities.GetListBySpec(new CitySpecs.GetAll()));

        public async Task<IEnumerable<CityDto>> GetByAreaIdAsync(int id) => mapper.Map<IEnumerable<CityDto>>(await cities.GetListBySpec(new CitySpecs.GetByAreaId(id))) 
            ?? throw new HttpException("Invalid area ID", HttpStatusCode.BadRequest);
        

        public async Task<CityDto> GetByIdAsync(int id) => mapper.Map<CityDto>(await cities.GetByIDAsync(id))
             ?? throw new HttpException("Invalid city ID", HttpStatusCode.BadRequest);
        
    }
}
