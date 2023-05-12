using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.FriendshipVM;
using MvcIBF.Repository.IRepository;
using System.Security.Claims;

namespace MvcIBF.Areas.User.Controllers
{
    [Area("User")]
    public class FriendshipsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;
        public FriendshipsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            var users = _context.ApplicationUser.GetAll();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName!.Contains(searchString));
            }
            var currentFriends = _context.Friendship.GetUserFriendsById(userId);
            
            users = users.Where(u => u.Id != currentUser.Id).Except(currentFriends);
            
            return View(users);
        }

        // GET: FriendshipController/Details/5
        public ActionResult Details(string id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            var friend = _context.ApplicationUser.GetById(id);
            return View();
        }

        // GET: FriendshipController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: FriendshipController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string FriendId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var user = _context.ApplicationUser.GetById(userId);
            var friend = _context.ApplicationUser.GetById(FriendId);
            var friendship = new Friendship
            {
                ApplicationUser1 = user,
                User1Id = userId,
                User2Id = FriendId,
                ApplicationUser2 = friend
            };
            user.Friendships.Add(friendship);
            friend.Friendships.Add(friendship);
            _context.Friendship.Add(friendship);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> FriendsList(string searchString)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            var users = _context.ApplicationUser.GetAll();
            var currentFriends = _context.Friendship.GetUserFriendsById(userId);
            if (currentFriends == null || currentFriends.Count == 0)
            {
                ModelState.AddModelError("", "Nie masz żadnych znajomych");
                return View("EmptyFriendsList");
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                currentFriends = currentFriends.Where(u => u.UserName.Contains(searchString)).ToList();
            }
            return View(currentFriends);
        }

        
        

        // POST: FriendshipController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string FriendId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            var users = _context.ApplicationUser.GetAll();
            var friend = _context.ApplicationUser.GetById(FriendId);
            var friendship = _context.Friendship.GetFriendshipById(userId, FriendId);
            currentUser.Friendships.Remove(friendship);
            friend.Friendships.Remove(friendship);
            _context.Friendship.Remove(friendship);
            _context.Save();
            return RedirectToAction(nameof(FriendsList));
        }
    }
}
