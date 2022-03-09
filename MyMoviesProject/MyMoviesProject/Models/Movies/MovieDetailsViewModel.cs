namespace MyMoviesProject.Models.Movies
{
    using MyMoviesProject.Services.Actors;
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;

    public class MovieDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Year { get; init; }

        public string ImageUrl { get; init; }

        public MovieDirectorServiceModel Director { get; init; }

        public string Storyline { get; init; }

        public IEnumerable<MovieGenresServiceModel> Genres { get; set; }

        public IEnumerable<ActorServiceModel> Actors { get; set; }

    }
}
