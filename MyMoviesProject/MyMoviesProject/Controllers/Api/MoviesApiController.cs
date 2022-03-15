namespace MyMoviesProject.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Infrastructure;
    using MyMoviesProject.Services.Movies;

    [Route("/api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieService movies;

        public MoviesApiController(IMovieService movies)
            => this.movies = movies;
        
        [HttpPost]
        [Authorize]
        public string Rate(int ratingValue, int movieId)
        {
           var userId = this.User.Id();
           this.movies.AddRating(movieId, userId, ratingValue);
           return userId;
        }

        [HttpGet]
        [Authorize]
        public decimal GetAverageRating(int movieId)
        {
            var averageRating = this.movies.GetAverageRating(movieId);
            return averageRating;
        }
    }
}
