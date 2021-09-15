﻿namespace MyMoviesProject.Infrastructure
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedGenres(services);

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
               new Genre {Name="Action"},
               new Genre {Name="Comedy"},
               new Genre {Name="Drama"},
               new Genre {Name="Fantasy"},
               new Genre {Name="Horror"},
               new Genre {Name="Mystery"},
               new Genre {Name="Romance"},
               new Genre {Name="Thriller"},
                new Genre {Name="Western"},
            });

            data.SaveChanges();
        }
    }
}