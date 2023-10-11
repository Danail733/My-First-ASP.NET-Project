using MyMoviesProject.Services.Movies;
using System.Collections.Generic;

namespace MyMoviesProject.Services.Shared
{
    public abstract class PersonServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public string Biography { get; init; }

        public IEnumerable<MovieServiceModel> Movies { get; set; }
    }
}
