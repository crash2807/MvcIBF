using Microsoft.AspNetCore.Identity;
using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser user);
        ApplicationUser GetById(string id);
        ApplicationUser GetUsersRecommendations(int friendshipId, int movieId);
        Task<IdentityRole> GetRoleByNameAsync(string roleName);
        List<ApplicationUser> GetUsersInRole(IdentityRole role);
        void AddUserRole(ApplicationUser user, IdentityRole role);
        void RemoveUserRole(ApplicationUser user, IdentityRole role);
    }
}