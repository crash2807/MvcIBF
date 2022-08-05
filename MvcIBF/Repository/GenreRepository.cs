using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private MvcIBFContext _db;
        public GenreRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Genre genre)
        {
            _db.Update(genre);
        }
    }
}
