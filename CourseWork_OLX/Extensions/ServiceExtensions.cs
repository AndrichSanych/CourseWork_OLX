using Microsoft.OpenApi.Models;

namespace CourseWork_OLX.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddMainServices(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            //services.AddSwaggerGen(setup =>
            //{
            //    // Include 'SecurityScheme' to use JWT Authentication
            //    var jwtSecurityScheme = new OpenApiSecurityScheme
            //    {
            //        BearerFormat = "JWT",
            //        Name = "JWT Authentication",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.Http,
            //        Scheme = JwtBearerDefaults.AuthenticationScheme,
            //        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

            //        Reference = new OpenApiReference
            //        {
            //            Id = JwtBearerDefaults.AuthenticationScheme,
            //            Type = ReferenceType.SecurityScheme
            //        }
            //    };

            //    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            //    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        { jwtSecurityScheme, Array.Empty<string>() }
            //    });

            //});
        }
    }
}
