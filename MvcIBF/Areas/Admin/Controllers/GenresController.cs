using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.GenreVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Admin.Controllers
{
    public class GenresController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public GenresController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: GenreController
        public ActionResult Index()
        {
            IEnumerable<Genre> genres = _context.Genre.GetAll();
            var vm = _mapper.Map<List<GenreVM>>(genres);
            return View(vm);
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int id)
        {
            if (_context.Genre.GetAll == null)
            {
                return NotFound();
            }

            var genre = _context.Genre
                .GetFirstOrDefault(m => m.GenreId == id);
            var vm = _mapper.Map<GenreVM>(genre);

            if (genre == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGenreVM vm)
        {
            var model = _mapper.Map<Genre>(vm);

            if (ModelState.IsValid)
            {
                _context.Genre.Add(model);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int id)
        {
            var genre = _context.Genre
                        .GetFirstOrDefault(m => m.GenreId == id);
            var vm = _mapper.Map<GenreVM>(genre);

            if (genre == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GenreVM genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vm = _mapper.Map<Genre>(genre);

                    _context.Genre.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreId))
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

            return View(genre);
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int id)
        {
            if ( _context.Genre.GetAll == null)
            {
                return NotFound();
            }

            var genre = _context.Genre
                .GetFirstOrDefault(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: GenreController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_context.Genre.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.Genres'  is null.");
            }
            var genre = _context.Genre
                .GetFirstOrDefault(m => m.GenreId == id);
            if (genre != null)
            {
                _context.Genre.Remove(genre);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool GenreExists(int id)
        {
            return _context.Genre.GetAll().Any(e => e.GenreId == id);
        }
    }
}
