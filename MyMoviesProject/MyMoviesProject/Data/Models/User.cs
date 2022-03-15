namespace MyMoviesProject.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public Watchlist Watchlist { get; init; } 

        public int? WatchlistId { get; init; }

        public IEnumerable<MovieRating> Rating { get; set; } = new List<MovieRating>();
    }
}
