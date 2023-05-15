using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        private MvcIBFContext _db;
        public RatingRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public List<Rating> GetRatingsByMovieId(int id)
        {
            return _db.Ratings.Where(r => r.MovieId == id).ToList();
        }

        public List<Rating> GetRatingsByUserId(string userId)
        {
            return _db.Ratings.Where(r => r.ApplicationUserId == userId).ToList();
        }
        public Rating GetRatingByUserIdAndMovieId(string userId, int movieId)
        {
            var rating =  _db.Ratings.Where(r => r.ApplicationUserId == userId && r.MovieId == movieId).FirstOrDefault();
            return rating;
        }
        public void Update(Rating rating)
        {
            _db.Update(rating);
        }
    }
}
