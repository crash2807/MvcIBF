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
        }

        public MovieVM GetMovieVM(int id)
        {
            MovieVM movie = _db.Movies.Where(x => x.MovieId == id).Select(m => new MovieVM()
            {
                MovieId= m.MovieId,
                MovieTitle = m.MovieTitle,
                MovieDescription = m.MovieDescription,
                ReleaseDate = m.ReleaseDate,
                Runtime = m.Runtime,
                VodNames = m.Movie_VODs.Select(n => n.VOD.VodName).ToList(),
                SelectedVODs= m.Movie_VODs.Select(n => n.VOD.VodId).ToArray(),
                MoodNames = m.Movie_Moods.Select(n => n.Mood.MoodName).ToList(),
                SelectedMoods = m.Movie_Moods.Select(n => n.Mood.MoodId).ToArray(),
                GenreNames = m.Movie_Genres.Select(n => n.Genre.GenreName).ToList(),
                SelectedGenres = m.Movie_Genres.Select(n => n.Genre.GenreId).ToArray(),
                CountryNames = m.Movie_Countries.Select(n => n.Country.CountryName).ToList(),
                SelectedCountries = m.Movie_Countries.Select(n => n.Country.CountryId).ToArray(),
                URLs=m.Materials.Select(n=>n.URL).ToList(),
                
                

            }).FirstOrDefault();
            return movie;
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
        public Movie GetMovie(int id)
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

            }).FirstOrDefault();
            return movie;
        }
    }
}
