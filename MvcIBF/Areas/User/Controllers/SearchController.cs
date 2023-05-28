using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcIBF.Models.ViewModels.MovieVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Areas.User.Controllers
{
    [Area("User")]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _context;
        private IMapper _mapper;
        public SearchController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult FilterMoviesForm()
        {
            var filterViewModel = new MovieFilterVM();

            // Fetch the necessary data for populating the form fields
            filterViewModel.VODs = new MultiSelectList(_context.VOD.GetAll(), "VodId", "VodName");
            filterViewModel.Countries = new MultiSelectList(_context.Country.GetAll(), "CountryId", "CountryName");
            filterViewModel.Genres = new MultiSelectList(_context.Genre.GetAll(), "GenreId", "GenreName");
            filterViewModel.Moods = new MultiSelectList(_context.Mood.GetAll(), "MoodId", "MoodName");

            return View(filterViewModel);
        }
        [HttpPost]
        public IActionResult FilterMoviesResult(MovieFilterVM filterViewModel)
        {
            var filteredMovies = _context.Movie.FilterMovies(                
                filterViewModel.ReleaseDateFrom,
                filterViewModel.ReleaseDateTo,
                filterViewModel.Runtime,                
                filterViewModel.SelectedVODs,
                filterViewModel.SelectedMoods,
                filterViewModel.SelectedCountries,
                filterViewModel.SelectedGenres
            );

            filterViewModel.FilteredMovies = filteredMovies;

            return View(filterViewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
