using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private MvcIBFContext _db;
        public ApplicationUserRepository(MvcIBFContext db) : base(db)
        {
            _db = db; 
        }

        public ApplicationUser GetById(string id)
        {
            return _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUsersRecommendations(int friendshipId, int movieId)
        {
            
            var recommendation = _db.Movie_Friendships.Where(r=> r.FriendshipId== friendshipId && r.MovieId == movieId).FirstOrDefault();
            if (recommendation != null)
            {
                var friendship = _db.Friendships.Where(r=> r.FriendshipId == friendshipId).First();
                var friend = _db.ApplicationUsers.Where(r => r.Id == friendship.User2Id).First();
                return friend;
            }
            else
            {
                return null;
            }
            
        }

        public void Update(ApplicationUser user)
        {
            _db.Update(user);
        }
    }
}
