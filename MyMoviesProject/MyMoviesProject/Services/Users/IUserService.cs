namespace MyMoviesProject.Services.Users
{
    public interface IUserService
    {
        public int CurrentUserRating(int movieId, string userId);
    }
}
