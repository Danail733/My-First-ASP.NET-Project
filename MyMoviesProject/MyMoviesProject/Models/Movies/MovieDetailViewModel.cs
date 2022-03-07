namespace MyMoviesProject.Models.Movies
{
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;

    public class MovieDetailViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Year { get; init; }

        public IEnumerable<MovieGenresServiceModel> Genres { get; set; }

    }
}
