namespace MyMoviesProject.Data.Models
{
    using System.Collections.Generic;

    public class Genre
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<MovieGenre> MovieActors { get; set; } = new List<MovieGenre>();
    }
}
