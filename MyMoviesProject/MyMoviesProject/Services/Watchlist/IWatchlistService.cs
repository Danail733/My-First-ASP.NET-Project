namespace MyMoviesProject.Services.Watchlist
{
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;

   public interface IWatchlistService
    {
        public IEnumerable<MovieServiceModel> Listing(string userId);

        public int Add(int id, string userId);

        public int Remove(int id, string userId);
    }
}
