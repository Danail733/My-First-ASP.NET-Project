namespace MyMoviesProject.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Infrastructure;
    using MyMoviesProject.Services.Users;

    [Route("/api/users")]
    public class UsersRatingApiController : ControllerBase
    {
        private readonly IUserService users;

        public UsersRatingApiController(IUserService users)
           =>this.users = users;
        

        [HttpGet]
        public int CurrentUserRating(int movieId)
        {
            var userId = this.User.Id();
            return this.users.CurrentUserRating(movieId, userId);
        }
    }
}
