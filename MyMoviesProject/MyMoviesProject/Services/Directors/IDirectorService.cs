namespace MyMoviesProject.Services.Directors
{
    using MyMoviesProject.Models.Directors;
    using System.Collections.Generic;

    public interface IDirectorService
    {
        public int Add(string name, string biography, string imageUrl);

        public bool IsDirectorExists(string name);

        public DirectorServiceModel Details(int id);

        public DirectorQueryServiceModel ListAll(int currentPage);

        public IEnumerable<DirectorListingServiceModel> GetAll();
    }
}
