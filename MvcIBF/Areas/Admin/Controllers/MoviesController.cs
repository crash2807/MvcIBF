using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.MovieVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Admin.Controllers
{
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
            //var vods = _context.VOD.GetAll();
            //var selectList = new List<SelectListItem>();
            //foreach(var item in vods)
            //{
            //    selectList.Add(new SelectListItem(item.VodName, item.VodId.ToString()));
            //}
            var movie = _context.Movie//.Include(c => c.VODs)
                .GetMovieVM(id);
            var vm = _mapper.Map<MovieVM>(movie);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

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
            var vm = new MovieVM
            {
                VODsList = selectListVods,
                MoodsList = selectListMoods
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
            var vm = new MovieVM
            {
                VODsList = selectList,
                MoodsList = selectListMoods
            };
            var movie = _context.Movie.GetMovieVM(id);
            movie.VODsList = selectList;
            movie.MoodsList = selectListMoods;
            vm  = _mapper.Map<MovieVM>(movie);

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
                    _context.Movie.AddProperties(movie,m);
                    
                    
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
