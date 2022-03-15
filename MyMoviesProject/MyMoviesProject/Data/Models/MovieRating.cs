namespace MyMoviesProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MovieRating
    {
        public int MovieId { get; init; }

        public Movie Movie { get; set; }

        public string UserId { get; init; }

        public User User { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
