namespace MyMoviesProject.Services.Actors
{
    using System.Collections.Generic;

    public class ActorQueryServiceModel
    {
        public int CurrentPage { get; set; } = 1;

        public int TotalActors { get; set; }

        public const int ActorsPerPage = 14;

        public IEnumerable<ActorListingServiceModel> Actors { get; set; }
    }
}
