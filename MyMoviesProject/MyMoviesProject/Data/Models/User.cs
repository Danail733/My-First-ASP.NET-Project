namespace MyMoviesProject.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public Watchlist Watchlist { get; init; } 

        public int? WatchlistId { get; init; }
    }
}
