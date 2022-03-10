namespace MyMoviesProject.Infrastructure
{
    using AutoMapper;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Movies;
    using MyMoviesProject.Services.Actors;
    using MyMoviesProject.Services.Movies;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Movie, MovieServiceModel>();
            this.CreateMap<Actor, ActorListingServiceModel>();
            this.CreateMap<Actor, ActorServiceModel>();
            this.CreateMap<MovieDetailsServiceModel, MovieFormModel>();
        }
    }
}
