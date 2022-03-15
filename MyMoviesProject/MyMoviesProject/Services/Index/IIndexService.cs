namespace MyMoviesProject.Services.Index
{
    using MyMoviesProject.Models.Home;
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;

   public interface IIndexService
    {
        public IEnumerable<MovieServiceModel> LastReleasedMovies();

        public IEnumerable<MovieServiceModel> TopRatedMovies();

        public IndexViewModel GetIndexViewModel(IEnumerable<MovieServiceModel> lastReleases,
          IEnumerable<MovieServiceModel> topRated);
    }
}
