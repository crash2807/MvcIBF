using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcIBF.Repository.IRepository;
using System.Security.Claims;

namespace MvcIBF.Areas.User.Controllers
{
    [Area("User")]
    public class RatingsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public RatingsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: RatingsController
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var ratings = _context.Rating.GetRatingsByUserId(userId);
            foreach (var rating in ratings)
            {
                var movie = _context.Movie.GetMovie(rating.MovieId);
                rating.Movie = movie;
            }
            return View(ratings.OrderBy(x=> x.Movie.MovieTitle));
        }

       

        // GET: RatingsController/Edit/5
        public async Task<IActionResult> Edit(int MovieId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var rating = _context.Rating.GetRatingsByMovieId(MovieId).Where(u => u.ApplicationUserId == userId).First();
            return View(rating);
        }

        // POST: RatingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int MovieId, int RatingValue, string Comment)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var rating = _context.Rating.GetRatingsByMovieId(MovieId).Where(u => u.ApplicationUserId == userId).First();
            if(rating.Comment == Comment && rating.RatingValue== RatingValue)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                rating.Comment = Comment;
                rating.RatingValue = RatingValue;
                _context.Rating.Update(rating);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            
            
        }

        // GET: RatingsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: RatingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int MovieId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var rating = _context.Rating.GetRatingsByMovieId(MovieId).Where(u => u.ApplicationUserId == userId).First();
            _context.Rating.Remove(rating);
            _context.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
