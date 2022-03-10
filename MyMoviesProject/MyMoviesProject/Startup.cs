namespace MyMoviesProject
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Infrastructure;
    using MyMoviesProject.Services.Actors;
    using MyMoviesProject.Services.Directors;
    using MyMoviesProject.Services.Index;
    using MyMoviesProject.Services.Movies;
    using MyMoviesProject.Services.Watchlist;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<MoviesDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options
                =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MoviesDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IIndexService, IndexService>();
            services.AddTransient<IWatchlistService, WatchlistService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.
               UseHttpsRedirection().
               UseStaticFiles().
               UseRouting().
               UseAuthentication().
               UseAuthorization().
               UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
