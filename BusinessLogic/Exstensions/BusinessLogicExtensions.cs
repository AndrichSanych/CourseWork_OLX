using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace BusinessLogic.Exstensions
{
    public static class BusinessLogicExtensions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<INewPostService, NewPostService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<IAdvertService, AdvertService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JWTService>();
            services.AddScoped<ICategoryService, CategoryService>();


        }
    }
}
