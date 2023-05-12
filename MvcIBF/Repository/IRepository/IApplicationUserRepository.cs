using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser user);
        ApplicationUser GetById(string id);
    }
}