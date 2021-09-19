namespace MyMoviesProject.Services.Directors
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;

    public class DirectorService : IDirectorService
    {
        private readonly MoviesDbContext data;

        public DirectorService(MoviesDbContext data) 
            => this.data = data;

        public int Add(string name, string biography, string imageUrl)
        {
            var director = new Director
            {
                Name = name,
                Biography = biography,
                ImageUrl = imageUrl
            };

            this.data.Directors.Add(director);
            this.data.SaveChanges();

          
            return director.Id;

        }
    }
}
