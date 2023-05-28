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
        bool CheckEntry(Movie movie);
        void Attach(Movie movie);
        List<Movie> FilterMovies(DateTime? releaseDateFrom, DateTime? releaseDateTo, int? runtime, List<int>? selectedVOD, List<int>? SelectedMoods, List<int>? SelectedCountries, List<int>? SelectedGenres);
    }
}
