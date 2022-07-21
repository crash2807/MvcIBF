using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IMovieRepository: IRepository<Movie>
    {
        void Update(Movie movie);
        
    }
}
