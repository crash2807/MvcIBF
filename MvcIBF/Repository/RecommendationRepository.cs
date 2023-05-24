using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class RecommendationRepository : Repository<Movie_Friendship>, IRecommendationRepository
    {
        private MvcIBFContext _db;
        public RecommendationRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public List<Movie_Friendship> GetRecommendationsByUserId(string userId)
        {
            var recommendations = _db.Movie_Friendships.Where(r => r.Friendship.User2Id == userId).ToList();
            if(recommendations == null || recommendations.Count ==0)
            {
                return null;
            }
            else
            {
                foreach(var recommendation in recommendations)
                {
                    recommendation.Friendship = _db.Friendships.Where(r => r.FriendshipId == recommendation.FriendshipId).First();
                }
                return recommendations;
            }
            
        }
    }
}
