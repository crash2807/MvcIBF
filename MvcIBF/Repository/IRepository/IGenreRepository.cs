using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IGenreRepository : IRepository<Genre>
    {
        void Update(Genre genre);
    }
}
