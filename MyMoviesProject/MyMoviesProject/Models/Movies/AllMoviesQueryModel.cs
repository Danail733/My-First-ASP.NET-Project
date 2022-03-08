namespace MyMoviesProject.Models.Movies
{
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllMoviesQueryModel
    {
        [Display(Name ="Search by text")]
        public string SearchTerm { get; init; }

        [Display(Name ="Sort by")]
        public MovieSorting Sorting { get; init; }

        public const int MoviesPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public string Genre { get; set; }

        public IEnumerable<MovieGenresServiceModel> Genres { get; set; }

        public int TotalMovies { get; set; }

        public IEnumerable<MovieServiceModel> Movies { get; set; }

    }
}
