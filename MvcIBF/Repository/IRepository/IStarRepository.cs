using MvcIBF.Models;
using MvcIBF.Models.ViewModels.StarVM;

namespace MvcIBF.Repository.IRepository
{
    public interface IStarRepository : IRepository<Star>
    {
        void Update(Star star);
        void AddStarWithCountry(CreateStarVM vm);
        void AddProperties(StarVM vm, Star star);
        void AddMovies(StarVM vm, Star star);

        StarVM GetStarVM(int id);
        Star GetStar(int id);
        void DeleteMovies(Star star, Movie movie, Function function);
    }
}
