using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IFunctionRepository : IRepository<Function>
    {
        void Update(Function function);
    }
}
