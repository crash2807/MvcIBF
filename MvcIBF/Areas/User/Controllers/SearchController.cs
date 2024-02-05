using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcIBF.Models.ViewModels.MovieVM;
using MvcIBF.Repository.IRepository;
using System.Security.Claims;

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

            // Pobieranie danych do list
            filterViewModel.VODs = new MultiSelectList(_context.VOD.GetAll(), "VodId", "VodName");
            filterViewModel.Countries = new MultiSelectList(_context.Country.GetAll(), "CountryId", "CountryName");
            filterViewModel.Genres = new MultiSelectList(_context.Genre.GetAll(), "GenreId", "GenreName");
            filterViewModel.Moods = new MultiSelectList(_context.Mood.GetAll(), "MoodId", "MoodName");

            return View(filterViewModel);
        }
        [HttpPost]
        public IActionResult FilterMoviesResult(MovieFilterVM filterViewModel)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var ratings = _context.Rating.GetRatingsByUserId(userId);
            var seenMovies = new List<Models.Movie>();
            
            foreach (var rating in ratings)
            {
                var movie = _context.Movie.GetMovie(rating.MovieId);
                rating.Movie = movie;
                seenMovies.Add(movie);
            }
            
            
            var filteredMovies = _context.Movie.FilterMovies(                
                filterViewModel.ReleaseDateFrom,
                filterViewModel.ReleaseDateTo,
                filterViewModel.Runtime,                
                filterViewModel.SelectedVODs,
                filterViewModel.SelectedMoods,
                filterViewModel.SelectedCountries,
                filterViewModel.SelectedGenres
            );
            if (filterViewModel.AllMovies.HasValue)
            {
                if (filterViewModel.AllMovies == true)
                {
                    filterViewModel.FilteredMovies = filteredMovies;
                }
                else
                {
                    var filteredMoviesWithoutSeenOnes = new HashSet<Models.Movie>(); 

                    foreach (var movie in filteredMovies)
                    {
                        var isSeen = false;

                        foreach (var movie2 in seenMovies)
                        {
                            if (movie.MovieId == movie2.MovieId)
                            {
                                isSeen = true;
                                break;
                            }
                        }

                        if (!isSeen)
                        {
                            filteredMoviesWithoutSeenOnes.Add(movie);
                        }
                    }

                    filterViewModel.FilteredMovies = filteredMoviesWithoutSeenOnes.ToList(); 

                }
            }
            else
            {
                filterViewModel.FilteredMovies = filteredMovies;
            }

            return View(filterViewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
