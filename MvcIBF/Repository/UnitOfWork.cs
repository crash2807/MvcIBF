using MvcIBF.Data;
using MvcIBF.Repository.IRepository;
using MvcIBF.Repository;

namespace MvcIBF.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MvcIBFContext _db;
        public UnitOfWork(MvcIBFContext db)
        {
            _db = db;
            VOD = new VODRepository(_db);
            Movie = new MovieRepository(_db);
        }
        public IVODRepository VOD { get; private set; }
        public IMovieRepository Movie { get; private set; }

        public void Save()
        {
            _db.SaveChanges(); 
        }
    } 
}
