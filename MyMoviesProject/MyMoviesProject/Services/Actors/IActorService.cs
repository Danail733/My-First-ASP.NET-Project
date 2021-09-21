namespace MyMoviesProject.Services.Actors
{
    public interface IActorService
    {
        public int Add(string name, string biography, string imageUrl);

        public bool IsActorExist(string name);
    }
}
