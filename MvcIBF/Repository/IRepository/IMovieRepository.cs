using MvcIBF.Models;
using MvcIBF.Models.ViewModels.MovieVM;

namespace MvcIBF.Repository.IRepository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        void Update(Movie movie);
        void AddMovieWithVOD(MovieVM vm);
        MovieVM GetMovieVM(int id);

        void AddVODs(MovieVM vm,Movie movie);
        Movie GetMovie(int id);
    }
}
