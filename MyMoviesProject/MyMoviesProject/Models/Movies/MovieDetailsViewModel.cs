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

        public decimal AverageRating { get; set; }

        public IEnumerable<MovieGenresServiceModel> Genres { get; set; }

        public IEnumerable<ActorListingServiceModel> Actors { get; set; }

    }
}
