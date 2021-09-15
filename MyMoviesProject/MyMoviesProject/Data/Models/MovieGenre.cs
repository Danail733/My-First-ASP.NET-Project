namespace MyMoviesProject.Data.Models
{
    public class MovieGenre
    {   
        public Movie Movie { get; set; }

        public int MovieId { get; init; }

        public Genre Genre { get; set; }

        public int GenreId { get; init; }
    }
}
