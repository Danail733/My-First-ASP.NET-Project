namespace MyMoviesProject.Models.Actors
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class ActorFormModel
    {
        [Required]
        [StringLength(HumanNameMaxLength, MinimumLength = HumanNameMinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(BiographyMaxLength, MinimumLength = BiographyMinLength)]
        public string Biography { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}
