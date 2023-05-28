using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;


namespace MvcIBF.Repository
{
    public class VODRepository : Repository<VOD>, IVODRepository
    {
        private MvcIBFContext _db;
        public VODRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public List<VOD> GetAllMovieVods()
        {
            return _db.Movies_VODs.Select(mv => mv.VOD)
            .Distinct()
            .ToList(); ;
            
        }

        public void Update(VOD vod)
        {
            _db.Update(vod);
        }
    }
}
