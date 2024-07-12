using AutoMapper;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Models.AccountModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Net;


namespace BusinessLogic.Services
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IValidator<RegisterUserModel> registerValidator;
        private readonly IJwtService jwtService;
        public AccountService(UserManager<User> userManager,
                                IMapper mapper,
                                IValidator<RegisterUserModel> registerValidator,
                                IJwtService jwtService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.registerValidator = registerValidator;
            this.jwtService = jwtService;
            
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
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new HttpException(string.Join(" ", result.Errors.Select(x => x.Description)), HttpStatusCode.BadRequest);
            await userManager.AddToRoleAsync(user, Roles.User);
        }
    }
}
