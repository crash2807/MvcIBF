using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        private MvcIBFContext _db;

        public FriendshipRepository(MvcIBFContext db) : base(db)
        {
            _db = db;
        }

        public Friendship GetFriendshipById(string userId, string friendId)
        {
            var friendship = _db.Friendships.Where(u=> u.User1Id == userId && u.User2Id == friendId).FirstOrDefault();
            return friendship;
        }

        public List<ApplicationUser> GetUserFriendsById(string userId)
        {
            var currentFriends = _db.Friendships.Where(u => u.User1Id == userId);
            var applicationUsers = new List<ApplicationUser>();
            foreach (var friend in currentFriends)
            {
                applicationUsers.Add(friend.ApplicationUser2);
            }
            return applicationUsers;
        }
    }
}
