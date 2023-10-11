namespace MyMoviesProject.Services.Movies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Movies;
    using MyMoviesProject.Services.Actors;
    
    public class MovieService : IMovieService
    {
        private readonly MoviesDbContext data;
        private readonly IMapper mapper;

        public MovieService(MoviesDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

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
                    GenreId = genreId,
                    Genre = this.data.Genres.FirstOrDefault(g => g.Id == genreId),
                });
            }

            foreach (var actorId in actorsIds)
            {
                movie.MovieActors.Add(new MovieActor
                {
                    MovieId = movie.Id,
                    ActorId = actorId,
                    Actor = this.data.Actors.FirstOrDefault(a => a.Id == actorId),
                });
            }

            this.data.Movies.Add(movie);
            this.data.SaveChanges();

            return movie.Id;
        }

        public MovieQueryServiceModel ListAllMovies(string searchTerm,
           MovieSorting sorting, string genre, int currentPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.ToLower()
                  .Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                moviesQuery = moviesQuery
                    .SelectMany(m => m.MovieGenres.Select(g => g.Genre),
                    (m, g) => new { Movie = m, Genre = g })
                     .Where(m => m.Genre.Name == genre).Select(m => m.Movie);
            }

            moviesQuery = sorting switch
            {
                MovieSorting.Name => moviesQuery.OrderBy(m => m.Name.Trim()),
                MovieSorting.Year => moviesQuery.OrderByDescending(m => m.Year),
                MovieSorting.Rating => moviesQuery.OrderByDescending(m => m.Rating.Sum(r => r.Rating) / (m.Rating.Count() + 0.1)),
                _ => moviesQuery.OrderByDescending(m => m.Id)
            };

            var totalMovies = moviesQuery.Count();

            var movies = GetMovies(moviesQuery.Skip((currentPage - 1)
                * AllMoviesQueryModel.MoviesPerPage).Take(AllMoviesQueryModel.MoviesPerPage));

            return new MovieQueryServiceModel
            {
                TotalMovies = totalMovies,
                CurrentPage = currentPage,
                Movies = movies
            };
        }

        public MovieFormServiceModel FormDetails(int id)
        {
            var movieDetails = this.data.Movies.Where(m => m.Id == id)
             .Select(m => new MovieFormServiceModel
             {
                 Id = m.Id,
                 Name = m.Name,
                 Year = m.Year,
                 ImageUrl = m.ImageUrl,
                 Storyline = m.Storyline,
                 DirectorId = m.DirectorId,
                 ActorsIds = m.MovieActors.Select(a => a.ActorId).ToArray(),
                 GenresIds = m.MovieGenres.Select(mg => mg.GenreId).ToArray(),
             }).FirstOrDefault();

            return movieDetails;
        }

        public int Edit(int id, string name, string imageUrl, int[] genresIds, int year,
            int directorId, int[] actorsIds, string storyline)
        {
            var movieData = this.data.Movies.Include(m => m.MovieActors)
                .Include(m => m.MovieGenres)
                .FirstOrDefault(m => m.Id == id);

            movieData.Name = name;
            movieData.Year = year;
            movieData.ImageUrl = imageUrl;
            movieData.Storyline = storyline;
            movieData.DirectorId = directorId;

            movieData.MovieGenres.Clear();
            movieData.MovieActors.Clear();

            foreach (var genreId in genresIds)
            {
                movieData.MovieGenres.Add(new MovieGenre
                {
                    MovieId = id,
                    GenreId = genreId,
                });
            }

            foreach (var actorId in actorsIds)
            {
                movieData.MovieActors.Add(new MovieActor
                {
                    MovieId = id,
                    ActorId = actorId
                });
            }

            this.data.SaveChanges();

            return movieData.Id;
        }

        public int Remove(int id)
        {
            var movie = this.data.Movies.FirstOrDefault(m => m.Id == id);

            foreach (var ma in this.data.MovieActors)
            {
                if (ma.MovieId == id)
                {
                    this.data.MovieActors.Remove(ma);
                }
            }

            foreach (var mg in this.data.MovieGenres)
            {
                if (mg.MovieId == id)
                {
                    this.data.MovieGenres.Remove(mg);
                }
            }

            this.data.SaveChanges();

            this.data.Movies.Remove(movie);

            this.data.SaveChanges();

            return movie.Id;
        }

        public MovieDetailsViewModel Details(int id)
        {
           var result = this.data.Movies.Where(m => m.Id == id)
              .Select(m => new MovieDetailsViewModel
              {
                  Id = m.Id,
                  Name = m.Name,
                  Year = m.Year,
                  ImageUrl = m.ImageUrl,
                  Director = new MovieDirectorServiceModel
                  {
                      Id = m.DirectorId,
                      Name = m.Director.Name
                  },
                  Storyline = m.Storyline,
                  Actors = m.MovieActors.Select(ma => new ActorListingServiceModel
                  {
                      Id = ma.ActorId,
                      Name = ma.Actor.Name,
                      ImageUrl = ma.Actor.ImageUrl
                  }).ToList(),
                  Genres = m.MovieGenres.Select(mg => new MovieGenresServiceModel
                  {
                      Id = mg.GenreId,
                      Name = mg.Genre.Name
                  }).ToList(),
              }).FirstOrDefault();

            var movie = this.data.Movies.Include(m => m.Rating)
                .FirstOrDefault(m => m.Id == id);

            if (movie.Rating != null && movie.Rating.Count() > 0 )
            {
                var avgRating =(decimal)movie.Rating.Sum(r => r.Rating) / movie.Rating.Count();
                result.AverageRating =Math.Round(avgRating, 2);
            }

            return result;
        }

        public void AddRating(int movieId, string userId, int rating)
        {
            var userRating = this.data.MovieRatings.FirstOrDefault(mr => mr.UserId == userId && mr.MovieId == movieId);
            if (userRating != null)
            {
                userRating.Rating = rating;
            }
            else
            {
                this.data.MovieRatings.Add(new MovieRating
                {
                    MovieId = movieId,
                    UserId = userId,
                    Rating = rating
                });
            }

            this.data.SaveChanges();
        }

        public bool IsMovieExists(string name) =>
            name != null ? this.data.Movies.Any(m => m.Name.ToLower() == name.ToLower()) : false;

        public bool isIdValid(int id)
            => this.data.Movies.Any(m => m.Id == id);

        public decimal GetAverageRating(int id)
        {
            var result = this.data.Movies.Include(m => m.Rating)
                .FirstOrDefault(m => m.Id == id);

                return result.Rating.Count() == 0? 0 :(decimal)result.Rating.Sum(r => r.Rating) / result.Rating.Count();
        }

        public IEnumerable<MovieGenresServiceModel> AllGenres()
            => this.data.Genres
            .Select(g => new MovieGenresServiceModel
            {
                Id = g.Id,
                Name = g.Name
            }).OrderBy(g => g.Name)
            .ToList();

        public bool GenreExists(int[] genreIds)
        {
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

        private IEnumerable<MovieServiceModel> GetMovies(IQueryable<Movie> movieQuery)
           => movieQuery.ProjectTo<MovieServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();
    }
}
