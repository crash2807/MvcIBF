using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.FunctionVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class FunctionsController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public FunctionsController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: FunctionController
        public ActionResult Index()
        {
            IEnumerable<Function> functions = _context.Function.GetAll();
            var sortedFunctions = functions.OrderBy(f => f.FunctionName);
            var vm = _mapper.Map<List<FunctionVM>>(sortedFunctions);
            return View(vm);
        }

        // GET: FunctionController/Details/5
        public ActionResult Details(int id)
        {
            if (_context.Function.GetAll == null)
            {
                return NotFound();
            }

            var function = _context.Function
                .GetFirstOrDefault(m => m.FunctionId == id);
            var vm = _mapper.Map<FunctionVM>(function);

            if (function == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: FunctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FunctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFunctionVM vm)
        {
            var model = _mapper.Map<Function>(vm);

            if (ModelState.IsValid)
            {
                _context.Function.Add(model);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: FunctionController/Edit/5
        public ActionResult Edit(int id)
        {
            var function = _context.Function
                        .GetFirstOrDefault(m => m.FunctionId == id);
            var vm = _mapper.Map<FunctionVM>(function);

            if (function == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: FunctionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FunctionVM function)
        {
            if (id != function.FunctionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vm = _mapper.Map<Function>(function);

                    _context.Function.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionExists(function.FunctionId))
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

            return View(function);
        }

        // GET: FunctionController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_context.Function.GetAll == null)
            {
                return NotFound();
            }

            var function = _context.Function
                .GetFirstOrDefault(m => m.FunctionId == id);
            if (function == null)
            {
                return NotFound();
            }

            return View(function);
        }

        // POST: FunctionController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_context.Function.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.Functions'  is null.");
            }
            var function = _context.Function
                .GetFirstOrDefault(m => m.FunctionId == id);
            if (function != null)
            {
                _context.Function.Remove(function);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool FunctionExists(int id)
        {
            return _context.Function.GetAll().Any(e => e.FunctionId == id);
        }
    }
}
