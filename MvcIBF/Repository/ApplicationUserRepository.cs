using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public void AddUserRole(ApplicationUser user, IdentityRole role)
        {
            var userRole = new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _db.UserRoles.Add(userRole);            
        }

        public ApplicationUser GetById(string id)
        {
            
            return _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public async Task<IdentityRole> GetRoleByNameAsync(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public List<ApplicationUser> GetUsersInRole(IdentityRole role)
        {
            var usersInRoleIds = _db.UserRoles.Where(ur => ur.RoleId == role.Id)
        .Select(ur => ur.UserId).ToList();
            var usersInRole = new List<ApplicationUser>();
            foreach(var userId in usersInRoleIds)
            {
                var user = _db.ApplicationUsers.Where(u => u.Id == userId).First();
                usersInRole.Add(user);
            }
            return usersInRole;
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

        public void RemoveUserRole(ApplicationUser user, IdentityRole role)
        {
            var userRole = _db.UserRoles.FirstOrDefault(ur =>
                ur.RoleId == role.Id && ur.UserId == user.Id);
            if (userRole != null)
            {
                _db.UserRoles.Remove(userRole);
            }
        }

        public void Update(ApplicationUser user)
        {
            _db.Update(user);
        }
    }
}
