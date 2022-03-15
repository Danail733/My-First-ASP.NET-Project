namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Models.Directors;
    using MyMoviesProject.Services.Directors;
    using static WebConstants;

    public class DirectorsController : Controller
    {   
        private readonly IDirectorService directors;

        public DirectorsController(IDirectorService directors)
            => this.directors = directors;

        public IActionResult Add()
            => View();

        [HttpPost]
        [Authorize(Roles =administratorRoleName)]
        public IActionResult Add(DirectorFormModel director)
        {
            if (this.directors.IsDirectorExists(director.Name))
            {
                this.ModelState.AddModelError(nameof(director.Name), "This director already exists");
            }

            if (!ModelState.IsValid)
            {
                return View(director);
            }

            this.directors.Add(director.Name, director.Biography, director.ImageUrl);

            return RedirectToAction("Details");
        }

        public IActionResult All(int currentPage)
        {
            var query = this.directors.ListAll(currentPage);

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var director = this.directors.Details(id);

            return View(director);
        }

    }
}
