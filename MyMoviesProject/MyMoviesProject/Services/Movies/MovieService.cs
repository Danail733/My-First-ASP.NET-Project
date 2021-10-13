﻿namespace MyMoviesProject.Services.Movies
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Movies;

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
                    GenreId = genreId,
                    Genre= this.data.Genres.FirstOrDefault(g=>g.Id==genreId),
                });
            }

            foreach (var actorId in actorsIds)
            {
                movie.MovieActors.Add(new MovieActor
                {
                    MovieId = movie.Id,
                    ActorId = actorId,
                    Actor=this.data.Actors.FirstOrDefault(a=>a.Id==actorId),                  
                });
            }

            this.data.Movies.Add(movie);
            this.data.SaveChanges();

            return movie.Id;
        }

        public MovieQueryServiceModel ListAllMovies(string searchTerm,
           MovieSorting sorting, int currentPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.ToLower()
                  .Contains(searchTerm.ToLower()));
            }

            moviesQuery = sorting switch
            {
                MovieSorting.Name => moviesQuery.OrderBy(m => m.Name.Trim()),
                MovieSorting.Year => moviesQuery.OrderByDescending(m => m.Year),
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

        public MovieDetailsServiceModel Details(int id)
        {
            var movieDetails = this.data.Movies.Where(m => m.Id == id)
                .Include(m => m.MovieActors).Include(m => m.MovieGenres)
             .Select(m => new MovieDetailsServiceModel
             {
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

        public void Edit(int id, string name, string imageUrl, int[] genresIds, int year,
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
        }
        public bool IsMovieExists(string name) =>
            name != null ? this.data.Movies.Any(m => m.Name.ToLower() == name.ToLower()) : false;


        public bool isIdValid(int id)
            => this.data.Movies.Any(m => m.Id == id);

        public IEnumerable<MovieGenresServiceModel> AllGenres()
            => this.data.Genres
            .Select(g => new MovieGenresServiceModel
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

        public bool GenreExists(int[] genreIds)
        {
            var allIds = this.data.Genres.Select(g => g.Id).ToArray();
            genreIds = allIds;

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

        public static IEnumerable<MovieServiceModel> GetMovies(IQueryable<Movie> movieQuery)
           => movieQuery.Select(m => new MovieServiceModel
           {
               Id = m.Id,
               Name = m.Name,
               Year = m.Year,
               ImageUrl = m.ImageUrl
           })
            .ToList();
    }
}