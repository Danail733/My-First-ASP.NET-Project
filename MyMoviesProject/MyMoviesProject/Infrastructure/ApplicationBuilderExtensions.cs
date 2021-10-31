namespace MyMoviesProject.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedGenres(services);
            SeedAdministor(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MoviesDbContext>();

            data.Database.Migrate();       
        }

        private static void SeedGenres(IServiceProvider services)
        {
            var data = services.GetRequiredService<MoviesDbContext>();

            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {          
               new Genre {Name="Adventure"},              
            });

            data.SaveChanges();
        }

        private static void SeedAdministor(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            Task.Run(async () =>
           {
               if (await roleManager.RoleExistsAsync(administratorRoleName))
               {
                   return;
               }

               var role = new IdentityRole { Name = administratorRoleName };

               await roleManager.CreateAsync(role);

               const string adminEmail = "admin@mymovies.com";
               const string adminPassword = "admin12";

               var user = new User
               {
                   Email = adminEmail,
                   UserName = adminEmail,
               };

               await userManager.CreateAsync(user, adminPassword);

               await userManager.AddToRoleAsync(user, role.Name);
               
           })
                .GetAwaiter()
                .GetResult();
        }
    }
}
