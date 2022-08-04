using MvcIBF.Models;
using MvcIBF.Models.ViewModels.MovieVM;

namespace MvcIBF.Repository.IRepository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        void Update(Movie movie);
        void AddMovieWithProperties(MovieVM vm);
        MovieVM GetMovieVM(int id);

        void AddProperties(MovieVM vm,Movie movie);
        Movie GetMovie(int id);
    }
}
