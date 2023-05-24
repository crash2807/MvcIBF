using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IRecommendationRepository : IRepository<Movie_Friendship>
    {
        List<Movie_Friendship> GetRecommendationsByUserId(string userId);
    }
}
