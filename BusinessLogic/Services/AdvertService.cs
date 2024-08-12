using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Entities.Filter;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Models.AdvertModels;
using BusinessLogic.Specifications;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Net;


namespace BusinessLogic.Services
{
    internal class AdvertService : IAdvertService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Advert> adverts;
        private readonly IRepository<Image> images;
        private readonly IRepository<FilterValue> values;
        private readonly IRepository<AdvertValue> advertValues;
        private readonly IImageService imageService;
        private readonly UserManager<User> userManager;
        private readonly IValidator<AdvertCreationModel> advertCreationModelValidator;
       
        public AdvertService(IMapper mapper,
            IRepository<Advert> adverts,
            IRepository<Image> images,
            IRepository<FilterValue> values,
            IRepository<AdvertValue> advertValues,
            IImageService imageService,
            UserManager<User> userManager,
            IValidator<AdvertCreationModel> validator)
       {
            this.mapper = mapper;
            this.adverts = adverts;
            this.images = images;
            this.values = values;
            this.advertValues = advertValues;
            this.imageService = imageService;
            this.userManager = userManager;
            this.advertCreationModelValidator = validator;
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

        public async Task UpdateAsync(AdvertCreationModel advertModel)
        {
            await advertCreationModelValidator.ValidateAndThrowAsync(advertModel);
            var advertImages = await images.GetListBySpec(new ImagesSpecs.GetByAdvertId(advertModel.Id)) 
                ?? throw new HttpException("Invalid advert id", HttpStatusCode.BadRequest);
            var newAdvert = mapper.Map<Advert>(advertModel);
            var oldValues = await advertValues.GetListBySpec(new AdvertValueSpecs.GetAdvertValues(advertModel.Id));
            foreach (var item in oldValues)
            {
                await advertValues.DeleteAsync(item.Id);
            }
            await values.SaveAsync();
            newAdvert.Values = (await values.GetListBySpec(new FilterSpecs.GetValues(advertModel.FilterValues)))
               .Select(x => new AdvertValue { AdvertId = advertModel.Id, FilterValue = x })
               .ToHashSet();
            adverts.Update(newAdvert);
            await adverts.SaveAsync();
            var deletedImages = advertImages.Where(x => !advertModel.ImageFiles.Any(z => z.FileName == x.Name));
            if (advertModel.ImageFiles.Count > 0) 
            {
                for (int i = 0; i < advertModel.ImageFiles.Count; i++)
                {
                    if (advertModel.ImageFiles[i].ContentType != "old-image")
                    {
                       await images.InsertAsync(new Image()
                        {
                            AdvertId = advertModel.Id,
                            Name = imageService.SaveImageAsync(advertModel.ImageFiles[i]).Result,
                            Priority = i
                        });
                    }
                    else 
                    {
                        foreach (var item in advertImages.Where(x=> !deletedImages.Any(z => z.Id == x.Id)))
                        {
                            if ((item.Name == advertModel.ImageFiles[i].FileName))
                            {
                                item.Priority = i;
                                images.Update(item);
                            }

                        }
                    }
                }
            }
                     
            if (deletedImages.Any())
            {
                foreach (var image in deletedImages)
                    images.Delete(image);
                imageService.DeleteImages(deletedImages.Select(x => x.Name));
            }
            await images.SaveAsync();
        }

        public async Task<SearchResult<Advert,AdvertDto>> GetByFilterAsync(SearchModel<Advert> filter)  =>
            await new SearchResult<Advert, AdvertDto>(adverts, mapper, filter).GetResult();
       

        public async Task<IEnumerable<AdvertDto>> getAdvertsAsync(int[] ids) => mapper.Map<IEnumerable<AdvertDto>>(await adverts.GetListBySpec(new AdvertSpecs.GetAdvertsByIds(ids)));
                
    }
}
