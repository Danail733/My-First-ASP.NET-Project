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
    using MyMoviesProject.Infrastructure;
    using MyMoviesProject.Services.Actors;
    using MyMoviesProject.Services.Directors;
    using MyMoviesProject.Services.Index;
    using MyMoviesProject.Services.Movies;

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
                .AddDefaultIdentity<IdentityUser>(options
                => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MoviesDbContext>();

            services.AddControllersWithViews();

            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IIndexService, IndexService>();
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
