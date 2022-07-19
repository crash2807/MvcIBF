using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Controllers
{
    public class VODsController : Controller
    {
        private readonly IVODRepository _context;

        public VODsController(IVODRepository context)
        {
            _context = context;
        }

        // GET: VODs
        public  IActionResult Index()
        {
            IEnumerable<VOD> vodsList = _context.GetAll();
            return View(vodsList);
        }

        // GET: VODs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GetAll == null)
            {
                return NotFound();
            }

            var vOD =  _context
                .GetFirstOrDefault(m => m.VodId == id);
            if (vOD == null)
            {
                return NotFound();
            }

            return View(vOD);
        }

        // GET: VODs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VodId,VodName")] VOD vOD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vOD);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vOD);
        }

        // GET: VODs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var vOD = _context
               .GetFirstOrDefault(m => m.VodId == id);
            if (vOD == null)
            {
                return NotFound();
            }
            return View(vOD);
        }

        // POST: VODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VodId,VodName")] VOD vOD)
        {
            if (id != vOD.VodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vOD);
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
            if (id == null || _context.GetAll == null)
            {
                return NotFound();
            }

            var vOD = _context
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
            if (_context.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.VODs'  is null.");
            }
            var vOD = _context
                .GetFirstOrDefault(m => m.VodId == id);
            if (vOD != null)
            {
                _context.Remove(vOD);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }
        
        private bool VODExists(int id)
        {
            return _context.GetAll().Any(e => e.VodId == id);
        }
        
    }
}
