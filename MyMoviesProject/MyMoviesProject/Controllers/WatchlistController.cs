namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Infrastructure;
    using MyMoviesProject.Services.Watchlist;

    public class WatchlistController : Controller
    {
        private readonly IWatchlistService watchlist;

        public WatchlistController(IWatchlistService watchlist) 
            => this.watchlist = watchlist;

        [Authorize]
        public IActionResult Movies()
        {
            var userId = this.User.Id();
            var model = this.watchlist.Listing(userId);
            return View(model);
        }

        public IActionResult Add(int id)
        {
            string userId = this.User.Id();
            this.watchlist.Add(id, userId);
            return RedirectToAction("Movies", "Watchlist");
        }
    }
}
