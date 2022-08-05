using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country country);
    }
}
