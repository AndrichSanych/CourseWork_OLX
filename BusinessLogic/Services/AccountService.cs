using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.AccountModels;
using BusinessLogic.Specifications;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;


namespace BusinessLogic.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IValidator<RegisterUserModel> registerValidator;
        private readonly IJwtService jwtService;
        private readonly IImageService imageService;
        private readonly IRepository<Advert> adverts;
        private readonly IRepository<UserAdvert> userAdverts;

        public AccountService(UserManager<User> userManager,
                                IMapper mapper,
                                IValidator<RegisterUserModel> registerValidator,
                                IJwtService jwtService,
                                IImageService imageService,
                                IRepository<Advert> adverts,
                                IRepository<UserAdvert> userAdverts)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.registerValidator = registerValidator;
            this.jwtService = jwtService;
            this.imageService = imageService;
            this.adverts = adverts;
            this.userAdverts = userAdverts;
        }

        private async Task<string> UpdateAccessTokensAsync(User user)
        {
            var claims = await jwtService.GetClaimsAsync(user);
            return jwtService.CreateToken(claims);
        }
        public async Task<AuthResponce> LoginAsync(AuthRequest model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid registration data", HttpStatusCode.BadRequest);

            return new()
            {
                AccessToken = jwtService.CreateToken(await jwtService.GetClaimsAsync(user))
            };
        }

        
        public async Task RegisterUserAsync(RegisterUserModel model)
        {
            registerValidator.ValidateAndThrow(model);

            if (await userManager.FindByEmailAsync(model.Email) != null)
                throw new HttpException("This email allready exist", HttpStatusCode.BadRequest);

            var user = mapper.Map<User>(model);
            user.RegisterDate = DateTime.Now;
            if(model.AvatarFile != null)
               user.Avatar = await imageService.SaveImageAsync(model.AvatarFile);
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new HttpException(string.Join(" ", result.Errors.Select(x => x.Description)), HttpStatusCode.BadRequest);
            await userManager.AddToRoleAsync(user, Roles.User);
        }

        public async Task ToggleFavoriteAdvert(int advertId, ClaimsPrincipal user)
        {
            var currentUser = await userManager.GetUserAsync(user) 
                ?? throw new HttpException("Invalid user", HttpStatusCode.InternalServerError);
            
            var advert  = await adverts.GetByIDAsync(advertId) 
                ?? throw new HttpException("Invalid advert id", HttpStatusCode.BadRequest);
            var userAdvert = await userAdverts.GetItemBySpec(new UserAdvertSpecs.GetByUserIdAdvertId(advert.Id,currentUser.Id));
            if (userAdvert != null)
                await userAdverts.DeleteAsync(userAdvert.Id);
            else
               await userAdverts.InsertAsync(new UserAdvert { AdvertId = advert.Id,UserId = currentUser.Id });
            await userAdverts.SaveAsync();
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id) 
                ?? throw new HttpException("Invalid user id", HttpStatusCode.BadRequest);
            return mapper.Map<UserDto>(user);
        }
    }
}
