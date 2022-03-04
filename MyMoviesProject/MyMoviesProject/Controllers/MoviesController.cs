namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Models.Movies;
    using MyMoviesProject.Services.Movies;
    using static WebConstants;

    public class MoviesController : Controller
    {
        private readonly IMovieService movies;

        public MoviesController(IMovieService movies)
            => this.movies = movies;

        [Authorize(Roles =administratorRoleName)]
        public IActionResult Add()
        {
            return View(new MovieFormModel
            {
                Genres = this.movies.AllGenres(),
                Directors = this.movies.AllDirectors(),
                Actors = this.movies.AllActors(),
            });
        }

        [HttpPost]
        public IActionResult Add(MovieFormModel movie)
        {
            if (!this.movies.GenreExists(movie.GenresIds))
            {
                this.ModelState.AddModelError(nameof(movie.GenresIds), "This genres does not exists!");
            }

            if (!this.movies.DirectorExists(movie.DirectorId))
            {
                this.ModelState.AddModelError(nameof(movie.DirectorId), "Director does not exists!");
            }

            if (this.movies.IsMovieExists(movie.Name))
            {
                this.ModelState.AddModelError(nameof(movie.Name), "This movie already exists!");
            }

            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.AllGenres();
                movie.Directors = this.movies.AllDirectors();
                movie.Actors = this.movies.AllActors();
                return View(movie);
            }

            this.movies.Add(movie.Name, movie.GenresIds, movie.ImageUrl, movie.Year,
                movie.DirectorId, movie.ActorsIds, movie.Storyline);

            return RedirectToAction("All", "Movies");
        }

        public IActionResult All([FromQuery] AllMoviesQueryModel query)
        {
            var queryResult = this.movies.ListAllMovies(query.SearchTerm, query.Sorting, query.Genre,
                query.CurrentPage);

            query.Movies = queryResult.Movies;
            query.TotalMovies = queryResult.TotalMovies;
            query.Genres = this.movies.AllGenres();

            return View(query);
        }

        [Authorize(Roles = administratorRoleName)]
        public IActionResult Edit(int id)
        {
            var movie = this.movies.Details(id);

            return View(new MovieFormModel
            {
                Name = movie.Name,
                Year = movie.Year,
                ImageUrl = movie.ImageUrl,
                Storyline = movie.Storyline,
                DirectorId = movie.DirectorId,
                ActorsIds = movie.ActorsIds,
                GenresIds = movie.GenresIds,
                Genres = this.movies.AllGenres(),
                Directors = this.movies.AllDirectors(),
                Actors = this.movies.AllActors()
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieFormModel movie)
        {
            if (!this.movies.GenreExists(movie.GenresIds))
            {
                this.ModelState.AddModelError(nameof(movie.GenresIds), "This genres does not exists!");
            }

            if (!this.movies.DirectorExists(movie.DirectorId))
            {
                this.ModelState.AddModelError(nameof(movie.DirectorId), "Director does not exists!");
            }

            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.AllGenres();
                movie.Directors = this.movies.AllDirectors();
                movie.Actors = this.movies.AllActors();
                return View(movie);
            }

            if (!this.movies.isIdValid(id))
            {
                return BadRequest();
            }

            this.movies.Edit(id, movie.Name, movie.ImageUrl, movie.GenresIds, movie.Year,
                movie.DirectorId, movie.ActorsIds, movie.Storyline);  
                  
            return RedirectToAction("All", "Movies");
        }

        [Authorize(Roles = administratorRoleName)]
        public IActionResult Remove(int id)
        {
            this.movies.Remove(id);

            return RedirectToAction("All");
        }
    }
}
