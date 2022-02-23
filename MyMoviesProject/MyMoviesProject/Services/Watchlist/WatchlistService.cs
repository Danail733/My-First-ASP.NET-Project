namespace MyMoviesProject.Services.Watchlist
{
    using Microsoft.EntityFrameworkCore;
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
        {
            if (!this.data.Watchlists.Where(w => w.UserId == userId).Any())
            {
                return new List<MovieServiceModel>();
            }

            var moviesData = this.data.Watchlists.Where(w => w.UserId == userId)
                .Include(w => w.Movies)
                .Select(w => w.Movies).FirstOrDefault();

            var user = this.data.Watchlists.Where(w => w.UserId == userId)
                .Select(w => w.User).FirstOrDefault();

            var movies = new List<MovieServiceModel>();

            foreach (var movie in moviesData)
            {
                var movieResult = new MovieServiceModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Year = movie.Year,
                    ImageUrl = movie.ImageUrl,
                };

                movies.Add(movieResult);
            }
            return movies;
        }

        public int Add(int id, string userId)
        {
            var movie = this.data.Movies.Where(m => m.Id == id).FirstOrDefault();

            var watchlistData = this.data.Watchlists.Include(w => w.Movies)
                .Where(w => w.UserId == userId)
                .FirstOrDefault();
            if (watchlistData == null)
            {
                var movies = new List<Movie>();
                movies.Add(movie);
                var watchlist = new Watchlist()
                {
                    UserId = userId,
                    Movies = movies,
                    User = this.data.Users.Find(userId),
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

        public int Remove(int id, string userId)
        {
            var movie = this.data.Movies.FirstOrDefault(m => m.Id == id);

            var watchist = this.data.Watchlists.Where(w => w.UserId == userId)
                .Include(w => w.Movies).FirstOrDefault();

            watchist.Movies.Remove(movie);

            this.data.SaveChanges();

            return movie.Id;
        }
    }
}

