using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.CountryVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountriesController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;

        public CountriesController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // GET: CountryController
        public ActionResult Index()
        {
            IEnumerable<Country> countries = _context.Country.GetAll();
            var sortedCountries = countries.OrderBy(c => c.CountryName);
            var vm = _mapper.Map<List<CountryVM>>(sortedCountries);
            return View(vm);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            if (_context.Country.GetAll == null)
            {
                return NotFound();
            }

            var country = _context.Country
                .GetFirstOrDefault(m => m.CountryId == id);
            var vm = _mapper.Map<CountryVM>(country);

            if (country == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCountryVM vm)
        {
            var model = _mapper.Map<Country>(vm);

            if (ModelState.IsValid)
            {
                _context.Country.Add(model);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            var country = _context.Country
                        .GetFirstOrDefault(m => m.CountryId == id);
            var vm = _mapper.Map<CountryVM>(country);

            if (country == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CountryVM country)
        {
            if (id != country.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vm = _mapper.Map<Country>(country);

                    _context.Country.Update(vm);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryId))
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

            return View(country);
        }

        // GET: CountryController/Delete/5
        public ActionResult Delete(int id)
        {
            if (_context.Country.GetAll == null)
            {
                return NotFound();
            }

            var country = _context.Country
                .GetFirstOrDefault(m => m.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: CountryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_context.Country.GetAll == null)
            {
                return Problem("Entity set 'MvcIBFContext.Countries'  is null.");
            }
            var country = _context.Country
                .GetFirstOrDefault(m => m.CountryId == id);
            if (country != null)
            {
                _context.Country.Remove(country);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool CountryExists(int id)
        {
            return _context.Country.GetAll().Any(e => e.CountryId == id);
        }
    }
}
