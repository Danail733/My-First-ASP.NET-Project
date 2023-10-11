namespace MyMoviesProject.Infrastructure
{
    using AutoMapper;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Movies;
    using MyMoviesProject.Services.Actors;
    using MyMoviesProject.Services.Movies;
    using System.Linq;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Movie, MovieServiceModel>();
            this.CreateMap<Actor, ActorListingServiceModel>();
            this.CreateMap<Actor, ActorServiceModel>()
                .ForMember(x => x.Movies, opt => opt.MapFrom(c => c.MovieActors.Where(ma => ma.ActorId == c.Id).Select(ma => ma.Movie)));
            this.CreateMap<MovieFormServiceModel, MovieFormModel>();
        }
    }
}
