using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private MvcIBFContext _db;
        public CountryRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Country country)
        {
            _db.Update(country);
        }
    }
}
