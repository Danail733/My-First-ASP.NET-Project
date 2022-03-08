namespace MyMoviesProject.Services.Actors
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Actors;
    using System.Collections.Generic;
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

        public IEnumerable<ActorListingViewModel> GetAll()
            => this.data.Actors.Select(a => new ActorListingViewModel
            {
                Id = a.Id,
                Name = a.Name,
                ImageUrl = a.ImageUrl
            }).OrderBy(a => a.Name.Trim())
            .ToList();

        public ActorServiceModel Details(int id)
            => this.data.Actors.Select(a => new ActorServiceModel
            {
                Id = a.Id,
                Name = a.Name,
                ImageUrl = a.ImageUrl,
                Biography = a.Biography
            }).FirstOrDefault(a => a.Id == id);
       
        public bool IsActorExist(string name)
            => name != null ? this.data.Actors.Any(a => a.Name.ToLower() == name.ToLower())
            : false;
    }
}
