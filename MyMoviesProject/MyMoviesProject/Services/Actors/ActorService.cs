namespace MyMoviesProject.Services.Actors
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using System.Linq;

    public class ActorService : IActorService
    {
        private readonly MoviesDbContext data;

        public ActorService(MoviesDbContext data)
            => this.data = data;

        public int Add(string name, string biography, string imageUrl)
        {
            var actor = new Actor
            {
                Name = name,
                Biography = biography,
                ImageUrl = imageUrl
            };

            this.data.Actors.Add(actor);
            this.data.SaveChanges();

            return actor.Id;
        }

        public bool IsActorExist(string name)
            => name != null ? this.data.Actors.Any(a => a.Name.ToLower() == name.ToLower())
            : false;
    }
}
