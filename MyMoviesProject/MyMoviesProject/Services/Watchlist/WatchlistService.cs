namespace MyMoviesProject.Services.Watchlist
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;
    using System.Linq;

    public class WatchlistService : IWatchlistService
    {
        private readonly MoviesDbContext data;

        public WatchlistService(MoviesDbContext data)
            => this.data = data;

        public IEnumerable<MovieServiceModel> Listing(string userId)
             => this.data.Watchlists.Where(w => w.UserId == userId)
            .Select(w => new MovieServiceModel
            {
                Id=w.Movies.Select(m=>m.Id).FirstOrDefault(),
                Name=w.Movies.Select(m=>m.Name).FirstOrDefault(),
                ImageUrl=w.Movies.Select(m=>m.ImageUrl).FirstOrDefault(),
                Year=w.Movies.Select(m=>m.Year).FirstOrDefault(),
            }).ToList();
    }
}

