namespace MyMoviesProject.Models.Movies
{
    using MyMoviesProject.Services.Movies;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class MovieFormModel
    {
        [Required]
        [StringLength(MovieNameMaxLength, MinimumLength = MovieNameMinLength)]
        public string Name { get; init; }

        [Display(Name ="Genre")]
        public int[] GenresIds { get; init; }

        public IEnumerable<MovieGenresServiceModel> Genres { get; set; }

        [Required]
        [Url]
        [Display(Name ="Image Url")]
        public string ImageUrl { get; init; }

        [Range(MovieYearMinValue, MovieYearMaxValue)]
        public int Year { get; init; }

        [Display(Name ="Director")]
        public int DirectorId { get; init; }

        public IEnumerable<MovieDirectorServiceModel> Directors { get; set; }

        [Display(Name="Actors")]
        public int[] ActorsIds { get; init; }

        public IEnumerable<MovieActorsServiceModel> Actors { get; set; }

        [Required]
        [StringLength(StorylineMaxValue, MinimumLength = StorylineMinValue)]
        public string Storyline { get; init; }
    }
}
