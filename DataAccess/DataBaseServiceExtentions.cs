using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataBaseServiceExtentions
    {
        public static void AddOlxDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OlxDbContext>(opts =>
                opts.UseNpgsql(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<OlxDbContext>();
        }
    }
}
