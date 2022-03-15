namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Models.Movies;
    using MyMoviesProject.Services.Movies;
    using AutoMapper;
    using static WebConstants;

    public class MoviesController : Controller
    {
        private readonly IMovieService movies;
        private readonly IMapper mapper;

        public MoviesController(IMovieService movies, IMapper mapper)
        {
            this.movies = movies;
            this.mapper = mapper;
        } 

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
        [Authorize(Roles = administratorRoleName)]
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

            TempData[WebConstants.SuccessFullGlobalMessageKey] = "The movie was edited successfully!";

            return RedirectToAction("Details", "Movies");
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
            var movie = this.movies.FormDetails(id);
            var movieFom = this.mapper.Map<MovieFormModel>(movie);
            movieFom.Genres = this.movies.AllGenres();
            movieFom.Directors = this.movies.AllDirectors();
            movieFom.Actors = this.movies.AllActors();

            return View(movieFom);
        }

        [HttpPost]
        [Authorize(Roles = administratorRoleName)]
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

            var movieId = this.movies.Edit(id, movie.Name, movie.ImageUrl, movie.GenresIds, movie.Year,
                movie.DirectorId, movie.ActorsIds, movie.Storyline);

            TempData[WebConstants.SuccessFullGlobalMessageKey] = "The movie was edited successfully!";
                  
            return RedirectToAction("All");
        }

        [Authorize(Roles = administratorRoleName)]
        public IActionResult Remove(int id)
        {
            this.movies.Remove(id);

            return RedirectToAction("All");
        }

        public IActionResult Details(int id)
        {
            var movie = this.movies.Details(id);

            return View(movie);
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
