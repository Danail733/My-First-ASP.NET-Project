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

        [Required]
        [StringLength(MovieActorsMaxValue, MinimumLength =MovieActorsMinValue)]
        public string Actors { get; init; }

        [Required]
        [StringLength(StorylineMaxValue, MinimumLength =StorylineMinValue)]
        public string Storyline { get; init; }
    }
}
