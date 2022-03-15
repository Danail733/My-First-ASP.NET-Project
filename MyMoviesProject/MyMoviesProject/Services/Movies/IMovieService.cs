namespace MyMoviesProject.Services.Movies
{
    using MyMoviesProject.Models.Movies;
    using System.Collections.Generic;

    public interface IMovieService
    {
        public int Add(string name, int[] genresIds, string imageUrl, int year,
            int directorId, int[] actorsIds, string storyline);

        public MovieQueryServiceModel ListAllMovies(string searchTerm,
    MovieSorting sorting, string genre, int currentPage);

        public MovieDetailsServiceModel FormDetails(int id);

        public int Edit(int id, string name, string imageUrl, int[] genresIds, int year,
            int directorId, int[] actorsIds, string storyline);

        public MovieDetailsViewModel Details(int id);

        public void AddRating(int movieId, string userId, int rating);

        public IEnumerable<MovieGenresServiceModel> AllGenres();

        public decimal GetAverageRating(int id);

        public bool GenreExists(int[] genreIds);

        public IEnumerable<MovieActorsServiceModel> AllActors();

        public bool ActorExists(int actorId);

        public IEnumerable<MovieDirectorServiceModel> AllDirectors();

        public bool DirectorExists(int directorId);

        public bool IsMovieExists(string name);

        public bool isIdValid(int id);

        public int Remove(int id);

    }
}
