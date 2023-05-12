using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IRatingRepository : IRepository<Rating>
    {
        List<Rating> GetRatingsByMovieId(int id);
        void Update(Rating rating);
        List<Rating> GetRatingsByUserId(string userId);
    }
}