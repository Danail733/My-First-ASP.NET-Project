namespace MyMoviesProject.Services.Movies
{
    using System.ComponentModel.DataAnnotations;

    public class MovieDetailsServiceModel :MovieServiceModel
    {
        public int[] GenresIds { get; set; }

        public int DirectorId { get; init; }

        public int[] ActorsIds { get; set; }

        public string Storyline { get; init; }
    }
}
