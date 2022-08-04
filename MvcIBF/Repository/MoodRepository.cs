using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;
using System.Linq.Expressions;

namespace MvcIBF.Repository
{
    public class MoodRepository : Repository<Mood> ,IMoodRepository
    {
        private MvcIBFContext _db;
        public MoodRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Mood mood)
        {
            _db.Update(mood);
        }
    }
}
