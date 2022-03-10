namespace MyMoviesProject.Services.Actors
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class ActorService : IActorService
    {
        private readonly MoviesDbContext data;
        private readonly IMapper mapper;

        public ActorService(MoviesDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }
            
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

        public ActorQueryServiceModel ListAll(int currentPage)
        {
            var actors = this.GetAll().Skip((currentPage - 1) * ActorQueryServiceModel.ActorsPerPage)
                .Take(ActorQueryServiceModel.ActorsPerPage).ToList();

            var result = new ActorQueryServiceModel
            {
                Actors = actors,
                TotalActors = this.data.Actors.Count(),
                CurrentPage = currentPage
            };

            if(result.CurrentPage == 0)
            {
                result.CurrentPage = 1;
            }

            return result;
        }

        public IEnumerable<ActorListingServiceModel> GetAll()
            => this.data.Actors
            .ProjectTo<ActorListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();

        public ActorServiceModel Details(int id)
            => this.data.Actors
            .ProjectTo<ActorServiceModel>(this.mapper.ConfigurationProvider)
             .FirstOrDefault(a => a.Id == id);
       
        public bool IsActorExist(string name)
            => name != null ? this.data.Actors.Any(a => a.Name.ToLower() == name.ToLower())
            : false;
    }
}
