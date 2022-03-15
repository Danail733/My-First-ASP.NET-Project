namespace MyMoviesProject.Services.Directors
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

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

        public DirectorQueryServiceModel ListAll(int currentPage = 1)
        {
            var directors = this.GetAll().Skip((currentPage - 1) * DirectorQueryServiceModel.DirectorsPerPage)
                .Take(DirectorQueryServiceModel.DirectorsPerPage).ToList();

            var result = new DirectorQueryServiceModel
            {
                TotalDirectors = this.data.Directors.Count(),
                CurrentPage = currentPage,
                Directors = directors
            };

            if(result.CurrentPage == 0)
            {
                result.CurrentPage = 1;
            }

            return result;
        }

        public IEnumerable<DirectorListingServiceModel> GetAll()
             => this.data.Directors.Select(d => new DirectorListingServiceModel
             {
                 Id = d.Id,
                 Name = d.Name,
                 ImageUrl = d.ImageUrl
             })
            .OrderBy(d => d.Name)
            .ToList();

        public DirectorServiceModel Details(int id)
            => this.data.Directors.Select(d => new DirectorServiceModel
            {
                Id = d.Id,
                Name = d.Name,
                ImageUrl = d.ImageUrl,
                Biography = d.Biography
            }).FirstOrDefault(d => d.Id == id);

        public bool IsDirectorExists(string name)
            => name != null ? this.data.Directors.Any(d => d.Name.ToLower() == name.ToLower())
            : false;
    }
}
