namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Models.Movies;
    using MyMoviesProject.Services.Movies;

    public class MoviesController : Controller
    {
        private readonly IMovieService movies;

        public MoviesController(IMovieService movies) 
            => this.movies = movies;

        public IActionResult Add()
        {
            return View(new MovieFormModel
            {
                Genres=this.movies.AllGenres(),              
                Directors=this.movies.AllDirectors(),
                Actors=this.movies.AllActors(),
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

            return RedirectToAction("Index", "Home");
        }

    }
}
