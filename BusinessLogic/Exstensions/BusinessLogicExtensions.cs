using BusinessLogic.Interfaces;
using BusinessLogic.Mapper;
using BusinessLogic.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Security.Claims;


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
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IFilterService, FilterService>();

        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameter1 = expr1.Parameters[0];
            var visitor = new ReplaceParameterVisitor(expr2.Parameters[0], parameter1);
            var body2WithParam1 = visitor.Visit(expr2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, body2WithParam1), parameter1);
        }
        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private ParameterExpression _oldParameter;
            private ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (ReferenceEquals(node, _oldParameter))
                    return _newParameter;

                return base.VisitParameter(node);
            }
        }
    }
}
