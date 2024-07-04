using BusinessLogic.Entities;
using BusinessLogic.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace CourseWork_OLX.Extensions
{
    public static class DBSeader
    {
        public static async Task SeedRoles(this IServiceProvider app)
        {
            var roleManager = app.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Roles.Get())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }

        public static async Task SeedAdmin(this IServiceProvider app)
        {
            var userManager = app.GetRequiredService<UserManager<User>>();

            const string USERNAME = "Admin@gmail.com";
            const string PASSWORD = "Admin_1";
            const string NAME = "Петро";
            const string SURNAME = "Левак";

            var existingUser = await userManager.FindByNameAsync(USERNAME);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = USERNAME,
                    Email = USERNAME,
                    Name = NAME,
                    Surname = SURNAME
                };

                var result = await userManager.CreateAsync(user, PASSWORD);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }
        }
        public static async Task SeedData(this IServiceProvider app)
        {
            
        }




    }

}
