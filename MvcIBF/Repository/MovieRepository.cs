using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private MvcIBFContext _db;
        public MovieRepository(MvcIBFContext db): base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _db.Update(movie);
        }
    }
}
