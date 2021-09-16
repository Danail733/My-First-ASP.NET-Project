namespace MyMoviesProject.Models.Directors
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class DirectorFormModel
    {
        [Required]
        [StringLength(HumanNameMaxLength, MinimumLength = HumanNameMinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(BiographyMaxLength, MinimumLength =BiographyMinLength)]
        public string Biography { get; init; }

        [Required]
        [Url]
        [Display(Name="Image Url")]
        public string ImageUrl { get; init; }
    }
}
