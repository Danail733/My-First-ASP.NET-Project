namespace MyMoviesProject.Services.Directors
{
    public interface IDirectorService
    {
        public int Add(string name, string biography, string imageUrl);

        public bool IsDirectorExists(string name);
    }
}
