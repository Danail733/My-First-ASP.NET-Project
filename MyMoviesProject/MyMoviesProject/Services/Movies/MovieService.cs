namespace MyMoviesProject.Services.Movies
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class MovieService : IMovieService
    {
        private readonly MoviesDbContext data;

        public MovieService(MoviesDbContext data)
            => this.data = data;

        public int Add(string name, int[] genresIds, string imageUrl, int year,
            int directorId, int[] actorsIds, string storyline)
        {
            var movie = new Movie
            {
                Name = name,
                ImageUrl = imageUrl,
                Year = year,
                DirectorId = directorId,
                Storyline = storyline
            };

            foreach (var genreId in genresIds)
            {
                movie.MovieGenres.Add(new MovieGenre
                {
                    MovieId = movie.Id,
                    GenreId = genreId
                });
            }

            foreach (var actorId in actorsIds)
            {
                movie.MovieActors.Add(new MovieActor
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                });
            }

            this.data.Movies.Add(movie);
            this.data.SaveChanges();

            return movie.Id;
        }
        public bool IsMovieExists(string name)
            => name != null ? this.data.Movies.Any(m => m.Name.ToLower() == name.ToLower())
            : false;


        public IEnumerable<MovieGenresServiceModel> AllGenres()
            => this.data.Genres
            .Select(g => new MovieGenresServiceModel
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();


        public bool GenreExists(int[] genreIds)
        {
            var allids = this.data.Genres.Select(g => g.Id).ToArray();
            genreIds = allids;

            foreach (var genreId in genreIds)
            {
                if (!this.data.Genres.Any(g => g.Id == genreId))
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<MovieActorsServiceModel> AllActors()
            => this.data.Actors
            .Select(a => new MovieActorsServiceModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();

        public bool ActorExists(int actorId)
            => this.data.Actors.Any(a => a.Id == actorId);

        public IEnumerable<MovieDirectorServiceModel> AllDirectors()
            => this.data.Directors
            .Select(d => new MovieDirectorServiceModel
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

        public bool DirectorExists(int directorId)
            => this.data.Directors
            .Any(d => d.Id == directorId);

    }
}
