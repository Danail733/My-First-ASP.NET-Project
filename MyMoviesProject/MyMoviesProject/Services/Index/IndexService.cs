namespace MyMoviesProject.Services.Index
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;
    using System.Linq;

    public class IndexService : IIndexService
    {
        private readonly MoviesDbContext data;

        public IndexService(MoviesDbContext data)
            => this.data = data;

        public IEnumerable<MovieServiceModel> LastReleasedMovies()
        {
            var movies = this.data.Movies.Select(m => new MovieServiceModel
            {
                Id=m.Id,
                Name = m.Name,
                Year = m.Year,
                ImageUrl = m.ImageUrl,
            })
                .OrderByDescending(m => m.Year)
                .Take(5).ToList();

            return movies;
        }
    }
}
