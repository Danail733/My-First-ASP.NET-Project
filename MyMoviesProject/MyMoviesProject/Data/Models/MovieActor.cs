namespace MyMoviesProject.Data.Models
{
    public class MovieActor
    {
        public Actor Actor { get; set; }

        public int ActorId { get; init; }

        public Movie Movie { get; set; }

        public int MovieId { get; init; }
    }
}
