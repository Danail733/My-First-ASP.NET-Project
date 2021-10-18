namespace MyMoviesProject.Data.Models
{
    using System.Collections.Generic;

    public class Watchlist
    {
        public int Id { get; init; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

        public string UserId { get; init; }

        public User User { get; init; }
    }
}
