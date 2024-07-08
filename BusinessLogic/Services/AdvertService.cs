using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.AdvertModels;
using BusinessLogic.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BusinessLogic.Services
{
    internal class AdvertService : IAdvertService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Advert> adverts;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;

        public AdvertService(IMapper mapper,
            IRepository<Advert> adverts,
            IImageService imageService,
            UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.adverts = adverts;
            this.imageService = imageService;
        }
        public async Task CreateAsync(AdvertCreationModel advertModel)  
        {
            if((await userManager.FindByIdAsync(advertModel.UserId))== null)
                 throw new HttpException("Invalid user ID", HttpStatusCode.BadRequest);

            var advert = mapper.Map<Advert>(advertModel);
            for (int i = 0;i < advertModel.ImageFiles.Count; i++)
            {
                advert.Images.Add(new Image()
                {
                    Name = await imageService.SaveImageAsync(advertModel.ImageFiles[i]),
                    Priority = i
                });
            }
            await adverts.InsertAsync(advert);
            await adverts.SaveAsync();
        }
        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdvertDto>> GetAllAsync() => mapper.Map<IEnumerable<AdvertDto>>(await adverts.GetListBySpec(new AdvertSpecs.GetAll()));
       
        public async Task<AdvertDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AdvertDto>> GetByUserEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AdvertUpdateModel advertModel)
        {
            throw new NotImplementedException();
        }
    }
}
