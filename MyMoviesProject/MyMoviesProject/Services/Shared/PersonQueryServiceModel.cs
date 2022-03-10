namespace MyMoviesProject.Services.Shared
{
    using System.Collections.Generic;

    public class PersonQueryServiceModel
    {
        public int CurrentPage { get; set; } = 1;

        public int TotalPersons { get; set; }

        public const int PersonsPerPage = 14;

        public IEnumerable<PersonListingViewModel> Persons { get; set; }
    }
}
