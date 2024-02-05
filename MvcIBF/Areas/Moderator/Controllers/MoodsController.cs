using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.MoodVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class MoodsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public MoodsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }
        // GET: MoodController
        public ActionResult Index()
        {
            IEnumerable<Mood> moods = _context.Mood.GetAll();
            var sortedMoods = moods.OrderBy(m => m.MoodName);
            var vm = _mapper.Map<List<MoodVM>>(sortedMoods);
            return View(vm);
        }

        // GET: MoodController/Details/5
        public ActionResult Details(int id)
        {
            if (_context.Mood.GetAll == null)
            {
                return NotFound();
            }

            var mood = _context.Mood
                .GetFirstOrDefault(m => m.MoodId == id);
            var vm = _mapper.Map<MoodVM>(mood);

            if (mood == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: MoodController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMoodVM vm)
        {
            var model = _mapper.Map<Mood>(vm);

            if (ModelState.IsValid)
            {
                _context.Mood.Add(model);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: MoodController/Edit/5
        public ActionResult Edit(int id)
        {
            var mood = _context.Mood
                        .GetFirstOrDefault(m => m.MoodId == id);
            var vm = _mapper.Map<MoodVM>(mood);

            if (mood == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: MoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MoodVM mood)
        {
            if (id != mood.MoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vm = _mapper.Map<Mood>(mood);

                    _context.Mood.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodExists(mood.MoodId))
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

            return View(mood);
        }

        // GET: MoodController/Delete/5
        public ActionResult Delete(int id)
        {
            if ( _context.Mood.GetAll == null)
            {
                return NotFound();
            }

            var mood = _context.Mood
                .GetFirstOrDefault(m => m.MoodId == id);
            if (mood == null)
            {
                return NotFound();
            }

            return View(mood);
        }

        // POST: MoodController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            if (_context.Mood.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.Moods'  is null.");
            }
            var mood = _context.Mood
                .GetFirstOrDefault(m => m.MoodId == id);
            if (mood != null)
            {
                _context.Mood.Remove(mood);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool MoodExists(int id)
        {
            return _context.Mood.GetAll().Any(e => e.MoodId == id);
        }
    }
}
