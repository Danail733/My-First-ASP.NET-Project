namespace MyMoviesProject.Services.Users
{
    using MyMoviesProject.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly MoviesDbContext data;

        public UserService(MoviesDbContext data)
          => this.data = data;
        

        public int CurrentUserRating(int movieId, string userId)
          => this.data.MovieRatings.Where(mr => mr.UserId == userId
            && mr.MovieId == movieId)
                .Select(mr => mr.Rating).FirstOrDefault();
        
    }
}
