using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private MvcIBFContext _db;
        public ApplicationUserRepository(MvcIBFContext db) : base(db)
        {
            _db = db; 
        }

        public ApplicationUser GetById(string id)
        {
            return _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public void Update(ApplicationUser user)
        {
            _db.Update(user);
        }
    }
}
