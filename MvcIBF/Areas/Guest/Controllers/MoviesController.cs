using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.MovieVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public MoviesController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = _context.Movie.GetAll();
            //var vods = _context.Movie//.Include(c => c.VODs) ;


            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.MovieTitle!.Contains(searchString));
            }
            var vm = _mapper.Map<List<MovieVM>>(movies);

            return View(vm);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_context.Movie.GetAll() == null)
            {
                return NotFound();
            }
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userId = claim.Value;
            var movie = _context.Movie
                .GetMovieVM(id);
            var vm = _mapper.Map<MovieVM>(movie);
            if (movie == null)
            {
                return NotFound();
            }
      //      var ratings = _context.Rating
      //.GetRatingsByMovieId(id)
      //.ToList();
        //    var currentUserRating = ratings
        //.SingleOrDefault(r => r.ApplicationUserId == userId);
        //    movie.Rating = currentUserRating?.RatingValue ?? 0;
        //    movie.UserComment = currentUserRating?.Comment ?? "Brak komentarza";
            

            return View(movie);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public async Task<IActionResult> Details(int id, int ratingValue, string comment)
        //{
        //    if (_context.Movie.GetAll() == null)
        //    {
        //        return NotFound();
        //    }
        //    var movieVM = _context.Movie
        //        .GetMovieVM(id);
        //    var movie = _context.Movie.GetMovie(id);
        //    var vm = _mapper.Map<MovieVM>(movieVM);
        //    if (movieVM == null)
        //    {
        //        return NotFound();
        //    }
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    var userId = claim.Value;
        //    var user = _context.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
        //    var rating = _context.Rating
        //        .GetFirstOrDefault(r => r.ApplicationUserId == userId && r.MovieId == movieVM.MovieId);
        //    if (rating == null)
        //    {
        //        rating = new Rating
        //        {
        //            ApplicationUserId = userId,
        //            MovieId = movie.MovieId,
        //            RatingValue = ratingValue,
        //            Comment = comment,
                    
        //        };
                
        //        _context.Rating.Add(rating);
        //        user.Ratings.Add(rating);
        //        movie.Ratings.Add(rating);
        //        _context.ApplicationUser.Update(user);
        //        //_context.Movie.Update(movie);
        //    }
        //    else
        //    {
        //        rating.RatingValue = ratingValue;
        //        rating.Comment = comment;
        //        _context.Rating.Update(rating);
        //    }

        //    _context.Save();

        //    return RedirectToAction("Details", new { id });
        //}
        // GET: Movies/Create
        public IActionResult Create()
        {
            var vods = _context.VOD.GetAll();
            var selectListVods = new List<SelectListItem>();
            foreach (var item in vods)
            {
                selectListVods.Add(new SelectListItem(item.VodName, item.VodId.ToString()));
            }
            var moods = _context.Mood.GetAll();
            var selectListMoods = new List<SelectListItem>();
            foreach (var item in moods)
            {
                selectListMoods.Add(new SelectListItem(item.MoodName, item.MoodId.ToString()));
            }
            var genres = _context.Genre.GetAll();
            var selectListGenres = new List<SelectListItem>();
            foreach (var item in genres)
            {
                selectListGenres.Add(new SelectListItem(item.GenreName, item.GenreId.ToString()));
            }
            var countries = _context.Country.GetAll();
            var selectListCountries = new List<SelectListItem>();
            foreach (var item in countries)
            {
                selectListCountries.Add(new SelectListItem(item.CountryName, item.CountryId.ToString()));
            }
            var functions = _context.Function.GetAll();
            var stars = _context.Star.GetAll();
            var selectListStars = new List<Star>();
            foreach (var item in stars)
            {
                var fullname = item.FirstName + " " + item.LastName;
                // selectListStars.Add(new SelectListItem(fullname, item.StarId.ToString()));
            }
            var selectedStars = new List<int>();
            var selectedStarsList = new List<List<int>>();
            foreach (var item in functions)
            {

                selectedStarsList.Add(selectedStars);
            }
            var vm = new MovieVM
            {
                VODsList = selectListVods,
                MoodsList = selectListMoods,
                GenresList = selectListGenres,
                CountriesList = selectListCountries,
                URLs = new List<string>(),
                Functions = functions,
                StarsList = stars,
                SelectedStarsFunction = selectedStarsList

            };
            return View(vm);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieVM vm)
        {
            if (ModelState.IsValid)
            {

                _context.Movie.AddMovieWithProperties(vm);
                var model = _mapper.Map<Movie>(vm);

                _context.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        //GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_context.Movie.GetAll() == null)
            {
                return NotFound();
            }
            var vods = _context.VOD.GetAll();
            var selectList = new List<SelectListItem>();
            foreach (var item in vods)
            {
                selectList.Add(new SelectListItem(item.VodName, item.VodId.ToString()));
            }
            var moods = _context.Mood.GetAll();
            var selectListMoods = new List<SelectListItem>();
            foreach (var item in moods)
            {
                selectListMoods.Add(new SelectListItem(item.MoodName, item.MoodId.ToString()));
            }
            var genres = _context.Genre.GetAll();
            var selectListGenres = new List<SelectListItem>();
            foreach (var item in genres)
            {
                selectListGenres.Add(new SelectListItem(item.GenreName, item.GenreId.ToString()));
            }
            var countries = _context.Country.GetAll();
            var selectListCountries = new List<SelectListItem>();
            foreach (var item in countries)
            {
                selectListCountries.Add(new SelectListItem(item.CountryName, item.CountryId.ToString()));
            }
            var urls = _context.Material.GetAll().Where(m => m.MovieId == id).Select(x => x.URL);
            var oldInput = String.Join(";", urls);

            var vm = new MovieVM
            {
                VODsList = selectList,
                MoodsList = selectListMoods,
                GenresList = selectListGenres,
                CountriesList = selectListCountries,
                InputURL = oldInput
            };
            var movie = _context.Movie.GetMovieVM(id);
            movie.VODsList = selectList;
            movie.MoodsList = selectListMoods;
            movie.GenresList = selectListGenres;
            movie.CountriesList = selectListCountries;
            movie.InputURL = oldInput;
            vm = _mapper.Map<MovieVM>(movie);

            if (movie == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieVM movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var m = _context.Movie.GetMovie(id);
                    var vm = _mapper.Map<Movie>(movie);
                    _context.Movie.AddProperties(movie, m);


                    _context.Movie.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie.GetAll() == null)
            {
                return NotFound();
            }

            var movie = _context.Movie
                .GetFirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie.GetAll() == null)
            {
                return Problem("Entity set 'MvcIBFContext.Movie'  is null.");
            }
            var movie = _context.Movie
                .GetFirstOrDefault(m => m.MovieId == id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.GetAll().Any(e => e.MovieId == id)).GetValueOrDefault();
        }

    }
}
