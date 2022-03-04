namespace MyMoviesProject.Services.Movies
{
    public class MovieDetailsServiceModel :MovieServiceModel
    {
        public int[] GenresIds { get; set; }

        public int DirectorId { get; init; }

        public int[] ActorsIds { get; set; }

        public string Storyline { get; init; }
    }
}
