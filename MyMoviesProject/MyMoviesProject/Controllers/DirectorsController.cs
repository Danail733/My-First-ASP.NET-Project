namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Data;
    using MyMoviesProject.Data.Models;
    using MyMoviesProject.Models.Directors;

    public class DirectorsController : Controller
    {
        private MoviesDbContext data;

        public DirectorsController(MoviesDbContext data) => this.data = data;
        
        public IActionResult Add()
            => View();

        [HttpPost]
        public IActionResult Add(DirectorFormModel director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            var directorData = new Director
            {
                Name = director.Name,
                Biography = director.Biography,
                ImageUrl = director.ImageUrl,
            };

            this.data.Directors.Add(directorData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
