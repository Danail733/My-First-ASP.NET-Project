namespace MyMoviesProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Movie
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MovieNameMaxLength)]
        public string Name { get; set; }

        public List<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        [Required]
        public string ImageUrl { get; set; }

        [Range(MovieYearMinValue,MovieYearMaxValue)]
        public int Year { get; init; }

        public Director Director { get; set; }

        public int DirectorId { get; set; }

        public List<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        
        [Required]
        public string Storyline { get; set; }

        

    }
}
