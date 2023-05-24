using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcIBF.Repository.IRepository;
using System.Security.Claims;

namespace MvcIBF.Areas.User.Controllers
{
    [Area("User")]
    public class RecommendationsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;
        public RecommendationsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var currentUser = _context.ApplicationUser.GetById(userId);
            var movies = _context.Movie.GetAll();
            var recommendations = _context.Recommendation.GetRecommendationsByUserId(userId);
            var friendship = _context.Friendship.GetAll();
            var users = _context.ApplicationUser.GetAll();
            if(recommendations == null)
            {
                return View("EmptyUserRecommendations");
            }
            return View(recommendations);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int MovieId, int friendshipId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var user = _context.ApplicationUser.GetById(userId);
            var recommendation = _context.Recommendation.GetAll().Where(r=> r.MovieId == MovieId && r.FriendshipId == friendshipId).FirstOrDefault();
            if(recommendation != null)
            {
                var friendship = _context.Friendship.GetAll().Where(f => f.FriendshipId == friendshipId).First();
                friendship.Movie_Friendships.Remove(recommendation);
                _context.Recommendation.Remove(recommendation);
                _context.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
