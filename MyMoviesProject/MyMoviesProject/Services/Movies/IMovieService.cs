namespace MyMoviesProject.Services.Movies
{
    using System.Collections.Generic;

    public interface IMovieService
    {
        public int Add(string name, int[] genresIds, string imageUrl, int year,
            int directorId, int[] actorsIds, string storyline);

        public IEnumerable<MovieGenresServiceModel> AllGenres();

        public bool GenreExists(int[] genreIds);

        public IEnumerable<MovieActorsServiceModel> AllActors();

        public bool ActorExists(int actorId);

        public IEnumerable<MovieDirectorServiceModel> AllDirectors();

        public bool DirectorExists(int directorId);

        public bool IsMovieExists(string name);


    }
}
