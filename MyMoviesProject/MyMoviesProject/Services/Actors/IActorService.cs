namespace MyMoviesProject.Services.Actors
{
    using System.Collections.Generic;

    public interface IActorService
    {
        public int Add(string name, string biography, string imageUrl);

        public ActorQueryServiceModel ListAll(int currentPage);

        public IEnumerable<ActorListingServiceModel> GetAll();

        public ActorServiceModel Details(int id);

        public bool IsActorExist(string name);
    }
}
