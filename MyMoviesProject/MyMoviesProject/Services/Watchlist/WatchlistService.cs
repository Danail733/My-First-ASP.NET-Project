namespace MyMoviesProject.Services.Watchlist
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
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

        public int Add(int id, string userId)
        {          
            var movie = this.data.Movies.Where(m => m.Id == id).FirstOrDefault();

            var watchlistData = this.data.Watchlists.Where(w => w.UserId == userId).FirstOrDefault();
            if (watchlistData==null)
            {
                var movies = new List<Movie>();
                movies.Add(movie);
                var watchlist = new Watchlist()
                {
                    UserId = userId,
                    Movies = movies,
                    User=this.data.Users.Where(u=>u.Id==userId).FirstOrDefault(),
                };
                this.data.Watchlists.Add(watchlist);
                this.data.SaveChanges();
            }
            else
            {
                watchlistData.Movies.Add(movie);
            }

            this.data.SaveChanges();
            return movie.Id;
        }
    }
}

