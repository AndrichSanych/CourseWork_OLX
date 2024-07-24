using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal class AreaService : IAreaService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Area> areas;

        public AreaService(IMapper mapper, IRepository<Area> areas)
        {
            this.mapper = mapper;
            this.areas = areas;
        }
        public async Task<IEnumerable<AreaDto>> GetAllAsync() => mapper.Map<IEnumerable<AreaDto>>(await areas.GetListBySpec(new AreaSpecs.GetAll()));


        public async Task<AreaDto> GetByCityIdAsync(int id) => mapper.Map<AreaDto>(await areas.GetItemBySpec(new AreaSpecs.GetByCityId(id)))
            ?? throw new HttpException("Invalid city ID", HttpStatusCode.BadRequest);


        public async Task<AreaDto> GetByIdAsync(int id) => mapper.Map<AreaDto>(await areas.GetByIDAsync(id))
             ?? throw new HttpException("Invalid area ID", HttpStatusCode.BadRequest);
    }
}

