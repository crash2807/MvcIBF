using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IMaterialRepository : IRepository<Material>
    {
        void Update(Material material);
    }
}
