using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.StarVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.User.Controllers
{
    [Area("User")]
    public class StarsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public StarsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: StarController
        public ActionResult Index()
        {
            IEnumerable<Star> stars = _context.Star.GetAll();
            var sortedStars = stars.OrderBy(s => s.LastName);
            var vm = _mapper.Map<List<StarVM>>(sortedStars);
            return View(vm);
        }

        // GET: StarController/Details/5
        public ActionResult Details(int id)
        {
            if (_context.Star.GetAll == null)
            {
                return NotFound();
            }

            var star = _context.Star
                .GetStarVM(id);
            
            //var vm = _mapper.Map<StarVM>(star);
            //vm.CountryName = _context.Country.GetAll().Where(x => x.CountryId == star.CountryId).Select(x => x.CountryName).ToString();
            if (star == null)
            {
                return NotFound();
            }
            //star.FunctionsList = star.FunctionsList.OrderBy(x => x.FunctionName);
            //star.MoviesList = star.MoviesList.OrderBy(x => x.MovieTitle);
            return View(star);
        }

        // GET: StarController/Create
        public ActionResult Create()
        {
            var countries = _context.Country.GetAll();
            var selectListCountries = new List<SelectListItem>();
            foreach (var item in countries)
            {
                selectListCountries.Add(new SelectListItem(item.CountryName, item.CountryId.ToString()));
            }
            var vm = new CreateStarVM
            {
                CountriesList = selectListCountries
            };
            return View(vm);
        }

        // POST: StarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStarVM vm)
        {
            var model = _mapper.Map<Star>(vm);

            if (ModelState.IsValid)
            {
                _context.Star.AddStarWithCountry(vm);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: StarController/Edit/5
        public ActionResult Edit(int id)
        {
            var star = _context.Star
                        .GetStarVM(id);
            var vm = _mapper.Map<StarVM>(star);
            var countries = _context.Country.GetAll();
            var selectListCountries = new List<SelectListItem>();
            foreach (var item in countries)
            {
                selectListCountries.Add(new SelectListItem(item.CountryName, item.CountryId.ToString()));
            }
            vm = new StarVM
            {
                CountriesList = selectListCountries
            };
            star.CountriesList = selectListCountries;
            if (star == null)
            {
                return NotFound();
            }
            return View(star);
        }

        // POST: StarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StarVM star)
        {
            if (id != star.StarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var s = _context.Star.GetStar(id);
                    var vm = _mapper.Map<Star>(star);
                    _context.Star.AddProperties(star,s);
                    //_context.Star.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarExists(star.StarId))
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

            return View(star);
        }

        // GET: StarController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_context.Star.GetAll == null)
            {
                return NotFound();
            }

            var star = _context.Star
                .GetFirstOrDefault(m => m.StarId == id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }

        // POST: StarController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_context.Star.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.Stars'  is null.");
            }
            var star = _context.Star
                .GetFirstOrDefault(m => m.StarId == id);
            if (star != null)
            {
                _context.Star.Remove(star);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool StarExists(int id)
        {
            return _context.Star.GetAll().Any(e => e.StarId == id);
        }
        // GET: StarController/AddMovies/5
        public ActionResult AddMovies(int id)
        {
            var star = _context.Star
                        .GetStarVM(id);
            var functions = _context.Function.GetAll();
            var movies = _context.Movie.GetAll();
            star.FunctionsList = functions;
            star.MoviesList = movies;
            return View(star);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovies(int id, StarVM star)
        {
            if (id != star.StarId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var s = _context.Star.GetStar(id);
                    var vm = _mapper.Map<Star>(star);
                    _context.Star.AddMovies(star, s);
                    //_context.Star.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarExists(star.StarId))
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
            return View(star);
        }

        // GET: StarController/DeleteMovies/5
        public ActionResult DeleteMovies(int id)
        {
            if (_context.Star.GetAll == null)
            {
                return NotFound();
            }

            var star = _context.Star
                .GetStarVM(id);
            if (star == null)
            {
                return NotFound();
            }

            return View(star);
        }
        [HttpPost, ActionName("DeleteMovies")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMoviesConfirmed(int StarId, int MovieId, int FunctionId)
        {
            if (_context.Star.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.Stars'  is null.");
            }
            var star = _context.Star
                .GetFirstOrDefault(m => m.StarId == StarId);
            //if (star != null)
            //{
            //    _context.Star.Remove(star);
            //}
            var movie = _context.Movie.GetFirstOrDefault(m => m.MovieId == MovieId);
            var function = _context.Function.GetFirstOrDefault(m => m.FunctionId == FunctionId);
            _context.Star.DeleteMovies(star,movie,function);
            return RedirectToAction(nameof(Index));
        }
    }
}
