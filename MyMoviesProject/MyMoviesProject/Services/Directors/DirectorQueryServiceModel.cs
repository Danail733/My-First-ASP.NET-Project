namespace MyMoviesProject.Services.Directors
{
    using System.Collections.Generic;

    public class DirectorQueryServiceModel
    {
        public int CurrentPage { get; set; } = 1;

        public int TotalDirectors { get; set; }

        public const int DirectorsPerPage = 14;

        public IEnumerable<DirectorListingServiceModel> Directors { get; set; }
    }
}
