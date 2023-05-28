using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.MovieVM;
using MvcIBF.Repository.IRepository;
using System.Linq;

namespace MvcIBF.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private MvcIBFContext _db;
        public MovieRepository(MvcIBFContext db): base(db)
        {
            _db = db;
        }

       
        public void Update(Movie movie)
        {
            _db.Update(movie);
            
        }

        public void AddMovieWithProperties(MovieVM movieVM)
        {
            var movie = new Movie()
            {
                MovieTitle = movieVM.MovieTitle,
                MovieDescription = movieVM.MovieDescription,
                ReleaseDate = movieVM.ReleaseDate,
                Runtime = movieVM.Runtime,
            };
            _db.Movies.Add(movie);
            _db.SaveChanges();
            if (movieVM.SelectedVODs != null)
            {
                foreach (var id in movieVM.SelectedVODs)
                {
                    var movieVOD = new Movie_VOD()
                    {
                        MovieId = movie.MovieId,
                        VODId = id
                    };
                    
                    _db.Movies_VODs.Add(movieVOD);
                    movie.Movie_VODs.Add(movieVOD);
                    _db.SaveChanges();
                    
                }
            }
            else
            {
                movie.Movie_VODs = null;
                _db.SaveChanges();
            }
            if (movieVM.SelectedMoods != null)
            {
                foreach (var id in movieVM.SelectedMoods)
                {
                    var movieMood = new Movie_Mood()
                    {
                        MovieId = movie.MovieId,
                        MoodId = id
                    };

                    _db.Movie_Moods.Add(movieMood);
                    movie.Movie_Moods.Add(movieMood);
                    _db.SaveChanges();

                }
            }
            else
            {
                movie.Movie_Moods = null;
                _db.SaveChanges();
            }
            if (movieVM.SelectedGenres != null)
            {
                foreach (var id in movieVM.SelectedGenres)
                {
                    var movieGenre = new Movie_Genre()
                    {
                        MovieId = movie.MovieId,
                        GenreId = id
                    };

                    _db.Movie_Genres.Add(movieGenre);
                    movie.Movie_Genres.Add(movieGenre);
                    _db.SaveChanges();

                }
            }
            else
            {
                movie.Movie_Genres = null;
                _db.SaveChanges();
            }
            if (movieVM.SelectedCountries != null)
            {
                foreach (var id in movieVM.SelectedCountries)
                {
                    var movieCountry = new Movie_Country()
                    {
                        MovieId = movie.MovieId,
                        CountryId = id
                    };

                    _db.Movie_Countries.Add(movieCountry);
                    movie.Movie_Countries.Add(movieCountry);
                    _db.SaveChanges();

                }
            }
            else
            {
                movie.Movie_Countries = null;
                _db.SaveChanges();
            }
            if(movieVM.InputURL != null)
            {
                var delimeter = ";";
                movieVM.URLs = movieVM.InputURL.Split(delimeter).ToList();
                foreach (var url in movieVM.URLs)
                {
                    var material = new Material()
                    {
                        MovieId=movie.MovieId,
                        URL = url
                    };
                    _db.Materials.Add(material);
                    movie.Materials.Add(material);
                    
                    _db.SaveChanges();
                }
            }
            else
            {
                movie.Materials = null;
                _db.SaveChanges();
            }
            //if (movieVM.SelectedStarsFunction != null)
            //{
            //    var MovieStars = new Movie_Star_Function
            //    {
            //        MovieId= movie.MovieId,
            //        FunctionId = movieVM.SelectedStarsFunction[],
            //    }
            //}
        }

        public MovieVM GetMovieVM(int id)
        {
            var movie = _db.Movies
        .Include(m => m.Movie_VODs).ThenInclude(mv => mv.VOD)
        .Include(m => m.Movie_Moods).ThenInclude(mm => mm.Mood)
        .Include(m => m.Movie_Genres).ThenInclude(mg => mg.Genre)
        .Include(m => m.Movie_Countries).ThenInclude(mc => mc.Country)
        .Include(m => m.Materials)
        .Include(m => m.Movie_Stars_Functions).ThenInclude(msf => msf.Star).ThenInclude(s => s.Country)
        .Include(m => m.Movie_Stars_Functions).ThenInclude(msf => msf.Function)
        .FirstOrDefault(x => x.MovieId == id);

            if (movie == null)
            {
                return null;
            }

            var movieVM = new MovieVM
            {
                MovieId = movie.MovieId,
                MovieTitle = movie.MovieTitle,
                MovieDescription = movie.MovieDescription,
                ReleaseDate = movie.ReleaseDate,
                Runtime = movie.Runtime,
                VodNames = movie.Movie_VODs.Select(n => n.VOD.VodName).ToList(),
                SelectedVODs = movie.Movie_VODs.Select(n => n.VOD.VodId).ToArray(),
                MoodNames = movie.Movie_Moods.Select(n => n.Mood.MoodName).ToList(),
                SelectedMoods = movie.Movie_Moods.Select(n => n.Mood.MoodId).ToArray(),
                GenreNames = movie.Movie_Genres.Select(n => n.Genre.GenreName).ToList(),
                SelectedGenres = movie.Movie_Genres.Select(n => n.Genre.GenreId).ToArray(),
                CountryNames = movie.Movie_Countries.Select(n => n.Country.CountryName).ToList(),
                SelectedCountries = movie.Movie_Countries.Select(n => n.Country.CountryId).ToArray(),
                URLs = movie.Materials.Select(n => n.URL).ToList(),
                StarsFunctionsList = movie.Movie_Stars_Functions.OrderBy(m => m.FunctionId).ToList(),
            };

            if (movieVM.StarsFunctionsList != null)
            {
                foreach (var msf in movieVM.StarsFunctionsList)
                {
                    msf.Star = new Star
                    {
                        StarId = msf.StarId,
                        FirstName = msf.Star.FirstName,
                        LastName = msf.Star.LastName,
                        ProfilePictureURL = msf.Star.ProfilePictureURL,
                        OtherName = msf.Star.OtherName,
                        DateOfBirth = msf.Star.DateOfBirth,
                        DateOfDeath = msf.Star.DateOfDeath,
                        Country = msf.Star.Country,
                        CountryId = msf.Star.CountryId
                    };

                    msf.Function = new Function
                    {
                        FunctionId = msf.FunctionId,
                        FunctionName = msf.Function.FunctionName
                    };
                }
            }

            var ratings = _db.Ratings.Where(r => r.MovieId == id);
            movieVM.AverageRating = ratings.Any() ? ratings.Average(r => r.RatingValue) : 0;

            return movieVM;
        }

        public void AddProperties(MovieVM vm, Movie movie)
        {
            
            if (vm.SelectedVODs != null)
            {                    
                var selectedVODs = vm.SelectedVODs;
                var existingVODs = movie.Movie_VODs.Select(x => x.VODId).Where(x=> movie.MovieId==vm.MovieId);
                var toAdd = selectedVODs.Except(existingVODs);
                var toRemove = existingVODs.Except(selectedVODs);
                foreach(var id in toRemove)
                {
                    var m = _db.Movies_VODs.First(x => x.MovieId==vm.MovieId && x.VODId==id);
                    
                    _db.Movies_VODs.Remove(m);
                    _db.SaveChanges();
                }
                foreach (var id in toAdd)
                {
                    var movieVOD = new Movie_VOD()
                    {
                        MovieId = vm.MovieId,
                        VODId = id
                    };
                    _db.Movies_VODs.Add(movieVOD);
                    _db.SaveChanges();
                }
            }
            else
            {                 
                _db.SaveChanges();
            }
            if (vm.SelectedMoods != null)
            {
                var selectedMoods = vm.SelectedMoods;
                var existingMoods = movie.Movie_Moods.Select(x => x.MoodId).Where(x => movie.MovieId == vm.MovieId);
                var toAdd = selectedMoods.Except(existingMoods);
                var toRemove = existingMoods.Except(selectedMoods);
                foreach (var id in toRemove)
                {
                    var m = _db.Movie_Moods.First(x => x.MovieId == vm.MovieId && x.MoodId == id);

                    _db.Movie_Moods.Remove(m);
                    _db.SaveChanges();
                }
                foreach (var id in toAdd)
                {
                    var movieMood = new Movie_Mood()
                    {
                        MovieId = vm.MovieId,
                        MoodId = id
                    };
                    _db.Movie_Moods.Add(movieMood);
                    _db.SaveChanges();
                }
            }
            else
            {
                _db.SaveChanges();
            }
            if (vm.SelectedGenres != null)
            {
                var selectedGenres = vm.SelectedGenres;
                var existingGenres = movie.Movie_Genres.Select(x => x.GenreId).Where(x => movie.MovieId == vm.MovieId);
                var toAdd = selectedGenres.Except(existingGenres);
                var toRemove = existingGenres.Except(selectedGenres);
                foreach (var id in toRemove)
                {
                    var m = _db.Movie_Genres.First(x => x.MovieId == vm.MovieId && x.GenreId == id);

                    _db.Movie_Genres.Remove(m);
                    _db.SaveChanges();
                }
                foreach (var id in toAdd)
                {
                    var movieGenre = new Movie_Genre()
                    {
                        MovieId = vm.MovieId,
                        GenreId = id
                    };
                    _db.Movie_Genres.Add(movieGenre);
                    _db.SaveChanges();
                }
            }
            else
            {
                _db.SaveChanges();
            }
            if (vm.SelectedCountries != null)
            {
                var selectedCountries = vm.SelectedCountries;
                var existingCountries = movie.Movie_Countries.Select(x => x.CountryId).Where(x => movie.MovieId == vm.MovieId);
                var toAdd = selectedCountries.Except(existingCountries);
                var toRemove = existingCountries.Except(selectedCountries);
                foreach (var id in toRemove)
                {
                    var m = _db.Movie_Countries.First(x => x.MovieId == vm.MovieId && x.CountryId == id);

                    _db.Movie_Countries.Remove(m);
                    _db.SaveChanges();
                }
                foreach (var id in toAdd)
                {
                    var movieCountry = new Movie_Country()
                    {
                        MovieId = vm.MovieId,
                        CountryId = id
                    };
                    _db.Movie_Countries.Add(movieCountry);
                    
                    _db.SaveChanges();
                }
            }
            else
            {
                _db.SaveChanges();
            }
            if (vm.InputURL != null)
            {
                var newInput = vm.InputURL;
                var urls = movie.Materials.Select(x => x.URL).Where(x => movie.MovieId == vm.MovieId);
                var oldInput = String.Join(";", urls);
                
                var compare = newInput.CompareTo(oldInput);
                if (compare != 0)
                {
                    var m = _db.Materials.Where(x => x.MovieId == vm.MovieId);
                    _db.Materials.RemoveRange(m);
                    if (newInput != null)
                    {
                        var delimeter = ";";
                        vm.URLs = newInput.Split(delimeter).ToList();
                        foreach (var url in vm.URLs)
                        {
                            var material = new Material()
                            {
                                MovieId = movie.MovieId,
                                URL = url
                            };
                            _db.Materials.Add(material);
                            movie.Materials.Add(material);

                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        _db.SaveChanges();
                    }
                }
            }
        }
        public Movie    GetMovie(int id)
        {
            Movie movie = _db.Movies.Where(x => x.MovieId == id).Select(m => new Movie()
            {
                MovieId = m.MovieId,
                MovieTitle = m.MovieTitle,
                MovieDescription = m.MovieDescription,
                ReleaseDate = m.ReleaseDate,
                Runtime = m.Runtime,
                Movie_VODs = m.Movie_VODs,
                Movie_Moods= m.Movie_Moods,
                Movie_Genres= m.Movie_Genres,
                Movie_Countries=m.Movie_Countries,
                Materials=m.Materials

            }).First();
            return movie;
        }

        public bool CheckEntry(Movie movie)
        {
            return _db.Entry(movie).IsKeySet;
        }

        public void Attach(Movie movie)
        {
            _db.Attach(movie);
        }

        public List<Movie> FilterMovies(DateTime? releaseDateFrom, DateTime? releaseDateTo, int? runtime, List<int>? SelectedVODs, List<int>? SelectedMoods, List<int>? SelectedCountries, List<int>? SelectedGenres)
        {
            var query = _db.Movies.Include(m => m.Movie_VODs)
                            .Include(m => m.Movie_Moods)
                            .Include(m => m.Movie_Genres)
                            .Include(m => m.Movie_Countries)
                            .AsQueryable();
            if (releaseDateFrom.HasValue)
            {
                query = query.Where(m => m.ReleaseDate.Date >= releaseDateFrom.Value.Date);
            }

            if (releaseDateTo.HasValue)
            {
                query = query.Where(m => m.ReleaseDate.Date <= releaseDateTo.Value.Date);
            }

            if (runtime.HasValue)
            {
                switch (runtime.Value)
                {
                    case 1: // < 60 min
                        query = query.Where(m => m.Runtime < 60);
                        break;
                    case 2: // < 90 min
                        query = query.Where(m => m.Runtime < 90);
                        break;
                    case 3: // < 120 min
                        query = query.Where(m => m.Runtime < 120);
                        break;
                    case 4: // > 120 min
                        query = query.Where(m => m.Runtime > 120);
                        break;
                    default:
                        break;
                }
            }
            if (SelectedVODs != null && SelectedVODs.Any())
            {
                query = query.Where(m => m.Movie_VODs.Any(v => SelectedVODs.Contains(v.VODId)));
            }
            if (SelectedGenres != null && SelectedGenres.Any())
            {
                query = query.Where(m => m.Movie_Genres.Any(v => SelectedGenres.Contains(v.GenreId)));
            }
            if (SelectedCountries != null && SelectedCountries.Any())
            {
                query = query.Where(m => m.Movie_Countries.Any(v => SelectedCountries.Contains(v.CountryId)));
            }
            if (SelectedMoods != null && SelectedMoods.Any())
            {
                query = query.Where(m => m.Movie_Moods.Any(v => SelectedMoods.Contains(v.MoodId)));
            }
            return query.ToList();
            
        }
    }
}
