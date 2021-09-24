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
            => View();

        [HttpPost]
        public IActionResult Add(ActorFormModel actor)
        {
            if (this.actors.IsActorExist(actor.Name))
            {
                this.ModelState.AddModelError(nameof(actor.Name), "This actor already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            this.actors.Add(actor.Name, actor.Biography, actor.ImageUrl);

            return RedirectToAction("Index", "Home");
        }


    }
}
