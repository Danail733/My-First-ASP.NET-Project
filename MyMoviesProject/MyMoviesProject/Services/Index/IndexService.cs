namespace MyMoviesProject.Services.Index
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Models.Home;
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
            var movies = this.data.Movies
                .Select(m => new MovieServiceModel
            {
                Id = m.Id,
                Name = m.Name,
                Year = m.Year,
                ImageUrl = m.ImageUrl,
            }).OrderByDescending(m => m.Year)
              .Take(5).ToList();

            return movies;
        }

        public IEnumerable<MovieServiceModel> TopRatedMovies()
            => this.data.Movies
                .OrderByDescending(m => m.Rating.Sum(r => r.Rating) / (m.Rating.Count() + 0.1))
                .Select(m => new MovieServiceModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Year = m.Year,
                    ImageUrl = m.ImageUrl,
                }).Take(5).ToList();

        public IndexViewModel GetIndexViewModel(IEnumerable<MovieServiceModel> lastReleases,
            IEnumerable<MovieServiceModel> topRated)
            => new IndexViewModel
            {
                LastRealeses = lastReleases,
                TopRated = topRated
            };
            
    }
}
