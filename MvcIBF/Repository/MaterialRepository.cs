using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private MvcIBFContext _db;
        public MaterialRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Material material)
        {
            _db.Update(material);
        }
    }
}
