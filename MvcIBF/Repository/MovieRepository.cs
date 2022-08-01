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

        public void AddMovieWithVOD(MovieVM movieVM)
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
                
            }).FirstOrDefault();
            return movie;
        }

        public void AddVODs(MovieVM vm, Movie movie)
        {
            
            if (vm.SelectedVODs != null)
            {
                    //var movie = _db.Movies;
                    
                var selectedVODs = vm.SelectedVODs;
                var existingVODs = movie.Movie_VODs.Select(x => x.VODId).Where(x=> movie.MovieId==vm.MovieId);
                var toAdd = selectedVODs.Except(existingVODs);
                var toRemove = existingVODs.Except(selectedVODs);
                foreach(var id in toRemove)
                {
                    var m = _db.Movies_VODs.First(x => x.MovieId==vm.MovieId && x.VODId==id);
                    //var movieVOD = new Movie_VOD()
                    //{
                    //    MovieId = vm.MovieId,
                    //    VODId = id
                    //};
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
                
                //movie.Movie_VODs = null;
                _db.SaveChanges();
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
                Movie_VODs = m.Movie_VODs

            }).FirstOrDefault();
            return movie;
        }
    }
}
