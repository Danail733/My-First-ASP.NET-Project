namespace MyMoviesProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyMoviesProject.Models.Actors;
    using MyMoviesProject.Services.Actors;

    public class ActorsController : Controller
    {
        private readonly IActorService actors;

        public ActorsController(IActorService actors) 
            => this.actors = actors;

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ActorFormModel actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            this.actors.Add(actor.Name, actor.Biography, actor.ImageUrl);

            return RedirectToAction("Index", "Home");
        }


    }
}
