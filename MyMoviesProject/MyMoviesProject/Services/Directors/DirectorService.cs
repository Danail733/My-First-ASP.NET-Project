namespace MyMoviesProject.Services.Directors
{
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Directors;
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

        public IEnumerable<DirectorListingViewModel> GetAll()
             => this.data.Directors.Select(d => new DirectorListingViewModel
             {
                 Id = d.Id,
                 Name = d.Name,
                 ImageUrl = d.ImageUrl
             }).OrderBy(d => d.Name)
            .ToList();

        public bool IsDirectorExists(string name)
            => name != null ? this.data.Directors.Any(d => d.Name.ToLower() == name.ToLower())
            : false;
    }
}
