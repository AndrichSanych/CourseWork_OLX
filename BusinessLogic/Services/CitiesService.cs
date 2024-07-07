using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<CityDto>> GetByAreaIdAsync(int id) => mapper.Map<IEnumerable<CityDto>>(await cities.GetListBySpec(new CitySpecs.GetByAreaId(id)));
        

        public async Task<CityDto> GetByIdAsync(int id) => mapper.Map<CityDto>(await cities.GetByIDAsync(id));
        
    }
}
