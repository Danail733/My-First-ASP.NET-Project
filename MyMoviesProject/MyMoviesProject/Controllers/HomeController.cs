namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Models;
    using MyMoviesProject.Services.Index;
    using System.Diagnostics;
 
    public class HomeController : Controller
    {
        private readonly IIndexService index;

        public HomeController(IIndexService index)
            => this.index = index;

        public IActionResult Index()
        {
            var lastMovies = index.LastReleasedMovies();
            var topRatedMovies = index.TopRatedMovies();

            var model = this.index.GetIndexViewModel(lastMovies, topRatedMovies);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
