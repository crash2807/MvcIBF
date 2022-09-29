using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;
using System.Linq.Expressions;

namespace MvcIBF.Repository
{
    public class FunctionRepository : Repository<Function>, IFunctionRepository
    {
        private MvcIBFContext _db;
        public FunctionRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Function function)
        {
            _db.Update(function);
        }
    }
}
