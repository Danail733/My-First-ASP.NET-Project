namespace MyMoviesProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Actor
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(HumanNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Biography { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public IEnumerable<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
