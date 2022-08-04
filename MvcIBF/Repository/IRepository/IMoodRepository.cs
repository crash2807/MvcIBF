using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IMoodRepository : IRepository<Mood>
    {
        void Update(Mood mood);
    }
}
