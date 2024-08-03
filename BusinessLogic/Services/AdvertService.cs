﻿using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Filter;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.AdvertModels;
using BusinessLogic.Specifications;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace BusinessLogic.Services
{
    internal class AdvertService : IAdvertService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Advert> adverts;
        private readonly IRepository<Image> images;
        private readonly IRepository<FilterValue> values;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;
        private readonly IValidator<AdvertCreationModel> advertCreationModelValidator;
        private readonly IValidator<AdvertUpdateModel> advertUpdateModelValidator;

        public AdvertService(IMapper mapper,
            IRepository<Advert> adverts,
            IRepository<Image> images,
            IRepository<FilterValue> values,
            IImageService imageService,
            UserManager<User> userManager,
            IValidator<AdvertCreationModel> validator,
            IValidator<AdvertUpdateModel> updateValidator)
        {
            this.mapper = mapper;
            this.adverts = adverts;
            this.images = images;
            this.values = values;
            this.imageService = imageService;
            this.userManager = userManager;
            this.advertCreationModelValidator = validator;
            this.advertUpdateModelValidator = updateValidator;
        }

        private async Task<Advert> getWithImage(int id) 
        {
            return  await adverts.GetItemBySpec(new AdvertSpecs.GetByIdWithImage(id)) ??
                throw new HttpException("Invalid advert id", HttpStatusCode.BadRequest);
        }

        public async Task CreateAsync(AdvertCreationModel advertModel)  
        {
            await advertCreationModelValidator.ValidateAndThrowAsync(advertModel);
            if ((await userManager.FindByIdAsync(advertModel.UserId))== null)
                 throw new HttpException("Invalid user ID", HttpStatusCode.BadRequest);

            var advert = mapper.Map<Advert>(advertModel);
            advert.Date = DateTime.Now;
            advert.Values = (await values.GetListBySpec(new FilterSpecs.GetValues(advertModel.FilterValues)))
                .Select(x=> new AdvertValue { FilterValue = x})
                .ToHashSet();
            for (int i = 0;i < advertModel.ImageFiles.Count; i++)
            {
                advert.Images.Add(new Image()
                {
                    Name = await imageService.SaveImageAsync(advertModel.ImageFiles[i]),
                    Priority = i
                });
            }
           
            try 
            {
                await adverts.InsertAsync(advert);
                await adverts.SaveAsync();
            }
            catch(Exception) 
            {
                imageService.DeleteImagesIfExists(advert.Images.Select(x=>x.Name));
                throw new HttpException("Error create advert",HttpStatusCode.InternalServerError);
            }
           
        }
        public async Task DeleteAsync(int id)
        {
            var advert = await getWithImage(id);
            adverts.Delete(advert);
            await adverts.SaveAsync();
            imageService.DeleteImages(advert.Images.Select(x=>x.Name));
        }

        public async Task<IEnumerable<AdvertDto>> GetAllAsync() => mapper.Map<IEnumerable<AdvertDto>>(await adverts.GetListBySpec(new AdvertSpecs.GetAll()));
       
        public async Task<AdvertDto> GetByIdAsync(int id) => mapper.Map<AdvertDto>(await adverts.GetItemBySpec(new AdvertSpecs.GetById(id)));
      
        public async Task<IEnumerable<AdvertDto>> GetByUserEmailAsync(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail) 
                ?? throw new HttpException("Invalid user email",HttpStatusCode.BadRequest);
            return mapper.Map<IEnumerable<AdvertDto>>(await adverts.GetListBySpec(new AdvertSpecs.GetByIdUserId(user.Id)));
        }

        public async Task<IEnumerable<AdvertDto>> GetVIPAsync(int count) => mapper.Map<IEnumerable<AdvertDto>>(await adverts.GetListBySpec(new AdvertSpecs.GetVIP(count)));


        public async Task<IEnumerable<ImageDto>> GetImagesAsync(int id)
        {
            var advert = await getWithImage(id);
            return mapper.Map<IEnumerable<ImageDto>>(advert.Images);
        }

        public async Task UpdateAsync(AdvertUpdateModel advertModel)
        {
            await advertUpdateModelValidator.ValidateAndThrowAsync(advertModel);
            var advertImages = await images.GetListBySpec(new ImagesSpecs.GetByAdvertId(advertModel.Id)) ??
                throw new HttpException("Invalid advert id", HttpStatusCode.BadRequest);
            var newAdvert = mapper.Map<Advert>(advertModel);
            adverts.Update(newAdvert);
            await adverts.SaveAsync();
            var deletedImages = advertImages.Where(x => !advertModel.Images.Any(z => z == x.Name));
            if (advertModel.ImageFiles.Count > 0) 
            {
                var newImages =  advertModel.ImageFiles.Select((x,i) => new Image()
                {
                    AdvertId = advertModel.Id,
                    Name = imageService.SaveImageAsync(advertModel.ImageFiles[i]).Result,
                    Priority = i  //???ToDo
                });
               await images.AddRangeAsync(newImages);
               await images.SaveAsync();
            }
                     
            if (deletedImages.Any())
            {
                foreach (var image in deletedImages)
                    images.Delete(image);
                await images.SaveAsync();
                imageService.DeleteImages(deletedImages.Select(x => x.Name));
            }
            
        }

        public async Task<SearchResult<Advert,AdvertDto>> GetByFilterAsync(AdvertSearchModel filter) 
        {
            var searcObject = new SearchResult<Advert, AdvertDto>(adverts,mapper,filter);
            return await searcObject.GetResult();
        }

        
    }
}
