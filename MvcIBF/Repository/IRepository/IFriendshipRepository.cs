using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        List<ApplicationUser> GetUserFriendsById(string userId);
        Friendship GetFriendshipById(string userId, string friendId);
    }
}
