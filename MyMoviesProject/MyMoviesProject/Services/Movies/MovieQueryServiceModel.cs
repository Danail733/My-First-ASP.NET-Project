namespace MyMoviesProject.Services.Movies
{
    using System.Collections.Generic;

    public class MovieQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int TotalMovies { get; init; }

        public IEnumerable<MovieServiceModel> Movies { get; init; }
    }
}
