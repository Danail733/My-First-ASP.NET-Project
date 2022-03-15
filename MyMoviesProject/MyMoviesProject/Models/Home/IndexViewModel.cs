namespace MyMoviesProject.Models.Home
{
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<MovieServiceModel> LastRealeses { get; set; }

        public IEnumerable<MovieServiceModel> TopRated { get; set; }
    }
}
