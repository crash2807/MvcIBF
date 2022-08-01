using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Admin.Controllers
{
    public class VODsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public VODsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: VODs
        public IActionResult Index()
        {
            IEnumerable<VOD> vodsList = _context.VOD.GetAll();
            var vm = _mapper.Map<List<VODVM>>(vodsList);

            return View(vm);
        }

        // GET: VODs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VOD.GetAll == null)
            {
                return NotFound();
            }

            var vOD = _context.VOD
                .GetFirstOrDefault(m => m.VodId == id);
            var vm = _mapper.Map<VODVM>(vOD);

            if (vOD == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        //GET: VODs/Create
        public IActionResult Create()
        {
            //VODVM vODVM = new()
            //{
            //    VOD = new(),
            //    MoviesList = _context.Movie.GetAll().Select(i => new SelectListItem
            //    {
            //        Text = i.MovieTitle,
            //        Value = i.MovieId.ToString()
            //    })
            //};
            return View();
        }

        // POST: VODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVODVM vm)
        {
            //VODVM vODVM = new()
            //{
            //    VOD = new(),
            //    MoviesList = _context.Movie.GetAll().Select(i => new SelectListItem
            //    {
            //        Text = i.MovieTitle,
            //        Value = i.MovieId.ToString()
            //    })
            //};
            var model = _mapper.Map<VOD>(vm);

            if (ModelState.IsValid)
            {
                _context.VOD.Add(model);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: VODs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VOD.GetAll() == null)
            {
                return NotFound();
            }
            /*
            IEnumerable<Movie> moviesList = _context.Movie.GetAll();
            var multiSelectList = new MultiSelectList(moviesList, "MovieId", "MovieTitle");
            //*/
            //IEnumerable<SelectListItem> listItems = _context.Movie.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.MovieTitle,
            //        Value = u.MovieId.ToString()
            //    }
            //);
            //ViewBag.MoviesList = listItems;
            var vOD = _context.VOD
               .GetFirstOrDefault(m => m.VodId == id);
            var vm = _mapper.Map<VODVM>(vOD);

            if (vOD == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: VODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VODVM vOD)
        {
            if (id != vOD.VodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vm = _mapper.Map<VOD>(vOD);

                    _context.VOD.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VODExists(vOD.VodId))
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

            return View(vOD);
        }

        // GET: VODs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VOD.GetAll == null)
            {
                return NotFound();
            }

            var vOD = _context.VOD
                .GetFirstOrDefault(m => m.VodId == id);
            if (vOD == null)
            {
                return NotFound();
            }

            return View(vOD);
        }

        // POST: VODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VOD.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.VODs'  is null.");
            }
            var vOD = _context.VOD
                .GetFirstOrDefault(m => m.VodId == id);
            if (vOD != null)
            {
                _context.VOD.Remove(vOD);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool VODExists(int id)
        {
            return _context.VOD.GetAll().Any(e => e.VodId == id);
        }

    }
}
