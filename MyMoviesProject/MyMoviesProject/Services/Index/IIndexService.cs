namespace MyMoviesProject.Services.Index
{
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;

   public interface IIndexService
    {
        public IEnumerable<MovieServiceModel> LastReleasedMovies();
    }
}
