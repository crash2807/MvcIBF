using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcIBF.Repository.IRepository;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcIBF.Models;

namespace MvcIBF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;
        
        public UsersController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var users = _context.ApplicationUser.GetAll();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName!.Contains(searchString));
            }
            users = users.Where(u => u.Id != currentUser.Id);
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string UserId)
        {
            var users = _context.ApplicationUser.GetAll();
            var user = _context.ApplicationUser.GetById(UserId);
            if (user != null)
            {
                var currentFriends = _context.Friendship.GetUserFriendsById(UserId);
                foreach (var friend in currentFriends)
                {
                    var friendship = _context.Friendship.GetFriendshipById(UserId, friend.Id);
                    friendship.Movie_Friendships.Clear();
                    friend.Friendships.Remove(friendship);
                    user.Friendships.Remove(friendship);
                    _context.Friendship.Remove(friendship);
                    
                    
                }
                var otherFriends = _context.Friendship.GetAll().Where(u => u.User2Id == UserId);
                foreach (var friend in otherFriends)
                {
                    _context.Friendship.Remove(friend);

                }
                _context.ApplicationUser.Remove(user);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> IndexModeratorsAdd(string searchString)
        {
            var users = _context.ApplicationUser.GetAll();
            var moderatorRole = await _context.ApplicationUser.GetRoleByNameAsync("Moderator");
            var moderators = _context.ApplicationUser.GetUsersInRole(moderatorRole);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName!.Contains(searchString));
            }
            
            users = users.Where(u => u.Id != currentUser.Id).Except(moderators);
            return View(users);
        }
        public async Task<IActionResult> AddModeratorRole(string userId)
        {
            var user = _context.ApplicationUser.GetById(userId);
            var moderatorRole = await _context.ApplicationUser.GetRoleByNameAsync("Moderator");
            
            if (user != null)
            {
                _context.ApplicationUser.AddUserRole(user, moderatorRole);
                _context.Save();
                return RedirectToAction("IndexModeratorsAdd");
            }
            else
            {
                return RedirectToAction("IndexModeratorsAdd");
            }
        }
        public async Task<IActionResult> IndexModeratorsDelete(string searchString)
        {
            var users = _context.ApplicationUser.GetAll();
            var moderatorRole = await _context.ApplicationUser.GetRoleByNameAsync("Moderator");
            var moderators = _context.ApplicationUser.GetUsersInRole(moderatorRole);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);            
            
            return View(moderators);
        }
        public async Task<IActionResult> RevokeModeratorRole(string userId)
        {
            var user = _context.ApplicationUser.GetById(userId);
            var moderatorRole = await _context.ApplicationUser.GetRoleByNameAsync("Moderator");
            var userRole = await _context.ApplicationUser.GetRoleByNameAsync("User");
            var usersInUserRole = _context.ApplicationUser.GetUsersInRole(userRole);
            if (user != null)
            {
                _context.ApplicationUser.RemoveUserRole(user, moderatorRole);
                if (!usersInUserRole.Contains(user))
                {
                    _context.ApplicationUser.AddUserRole(user, userRole);
                }
                _context.Save();
                return RedirectToAction("IndexModeratorsDelete");
            }
            else
            {
                return RedirectToAction("IndexModeratorsDelete");
            }
        }
    }
}
