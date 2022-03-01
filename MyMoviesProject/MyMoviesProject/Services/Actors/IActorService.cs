namespace MyMoviesProject.Services.Actors
{
    using MyMoviesProject.Models.Actors;
    using System.Collections.Generic;

    public interface IActorService
    {
        public int Add(string name, string biography, string imageUrl);

        public bool IsActorExist(string name);

        public IEnumerable<ActorListingViewModel> GetAll();
    }
}
