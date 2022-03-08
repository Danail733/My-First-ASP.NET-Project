namespace MyMoviesProject.Services.Actors
{
    using MyMoviesProject.Models.Actors;
    using System.Collections.Generic;

    public interface IActorService
    {
        public int Add(string name, string biography, string imageUrl);

        public IEnumerable<ActorListingViewModel> GetAll();

        public ActorServiceModel Details(int id);

        public bool IsActorExist(string name);
    }
}
