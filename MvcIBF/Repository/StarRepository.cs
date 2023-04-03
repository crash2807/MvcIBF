using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels.StarVM;
using MvcIBF.Repository.IRepository;

namespace MvcIBF.Repository
{
    public class StarRepository : Repository<Star>, IStarRepository
    {

        private MvcIBFContext _db;
        public StarRepository(MvcIBFContext db): base(db)
        {
            _db = db;
        }

        public void AddStarWithCountry(CreateStarVM vm)
        {
            var star = new Star
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                OtherName = vm.OtherName,
                DateOfBirth = vm.DateOfBirth,
                DateOfDeath = vm.DateOfDeath,
                ProfilePictureURL = vm.ProfilePictureURL,
                CountryId=vm.SelectedCountry
            };
            _db.Stars.Add(star);
            _db.SaveChanges();
            if (vm.SelectedCountry != null)
            {
                var country = _db.Countries.Where(x => x.CountryId == vm.SelectedCountry).FirstOrDefault();
                star.Country = country;
                star.CountryId = country.CountryId;
                country.Stars.Add(star);
                _db.SaveChanges();
            }
        }

        public StarVM GetStarVM(int id)
        {
            StarVM star = _db.Stars.Where(x => x.StarId == id).Select(x => new StarVM()
            {
                StarId = x.StarId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ProfilePictureURL = x.ProfilePictureURL,
                DateOfBirth = x.DateOfBirth,
                DateOfDeath = x.DateOfDeath,
                OtherName = x.OtherName,
                SelectedCountry = x.CountryId,
                CountryName = x.Country.CountryName,
                MoviesFunctionsList = x.Movie_Stars_Functions
                
            }).First();
            if (star.MoviesFunctionsList != null)
            {
                foreach (var Movie_Star_Function in star.MoviesFunctionsList)
                {
                    var MovieId = Movie_Star_Function.MovieId;
                    Movie movie = _db.Movies.Where(x => x.MovieId == MovieId).Select(m => new Movie()
                    {
                        MovieId = m.MovieId,
                        MovieTitle = m.MovieTitle,
                        MovieDescription = m.MovieDescription,
                        ReleaseDate = m.ReleaseDate,
                        Runtime = m.Runtime,
                        Movie_VODs = m.Movie_VODs,
                        Movie_Moods = m.Movie_Moods,
                        Movie_Genres = m.Movie_Genres,
                        Movie_Countries = m.Movie_Countries,
                        Materials = m.Materials
                    }).First();
                    Movie_Star_Function.Movie = movie;
                    var FunctionId = Movie_Star_Function.FunctionId;
                    Function function = _db.Functions.Where(x => x.FunctionId == FunctionId).Select(f => new Function()
                    {
                        FunctionId = f.FunctionId,
                        FunctionName = f.FunctionName
                    }).First();
                    Movie_Star_Function.Function = function;
                }
            }
            return star;
            
        }
        public void AddProperties(StarVM vm, Star star)
        {
            if(vm.FirstName != star.FirstName)
            {
                star.FirstName = vm.FirstName;
            }
            if(vm.LastName != star.LastName)
            {
                star.LastName = vm.LastName;
                
            }
            if (vm.OtherName != star.OtherName)
            {
                star.OtherName= vm.OtherName;
            }
            if(vm.ProfilePictureURL != star.ProfilePictureURL)
            {
                star.ProfilePictureURL=vm.ProfilePictureURL;
            }
            if(vm.DateOfBirth != star.DateOfBirth)
            {
                star.DateOfBirth=vm.DateOfBirth;
            }
            if(vm.DateOfDeath != star.DateOfDeath)
            {
                star.DateOfDeath = vm.DateOfDeath;
            }
            _db.Update(star);
            _db.SaveChanges();
            if (vm.SelectedCountry != star.CountryId)
            {
                var oldCountry = _db.Countries.Where(x => x.CountryId == star.CountryId).First();
                var country = _db.Countries.Where(x => x.CountryId == vm.SelectedCountry).First();
                star.Country = country;
                star.CountryId = country.CountryId;
                if (oldCountry.Stars.Count() != 0)
                {
                    oldCountry.Stars.Remove(star);
                }
                country.Stars.Add(star);
                _db.SaveChanges();

            }
        }
        public void AddMovies(StarVM vm, Star star)
        {
            var movie = _db.Movies.Where(x => x.MovieId == vm.SelectedMovie).First();
            var function = _db.Functions.Where(x => x.FunctionId == vm.SelectedFunction).First();
            var msf = new Movie_Star_Function
            {
                MovieId = movie.MovieId,
                StarId = star.StarId,
                FunctionId = function.FunctionId
            };
            msf.Movie = movie;
            msf.Function = function;
            msf.Star = star;
            var all = _db.Movie_Stars_Functions;
            if(!all.Contains(msf))
            {
                star.Movie_Stars_Functions.Add(msf);
                movie.Movie_Stars_Functions.Add(msf);
                function.Movie_Stars_Functions.Add(msf);
                _db.SaveChanges();
            }


        }


        public Star GetStar(int id)
        {
            var star = _db.Stars.Where(x => x.StarId == id).Select(s => new Star()
            {
                StarId= s.StarId,
                ProfilePictureURL= s.ProfilePictureURL,
                FirstName = s.FirstName,
                DateOfBirth= s.DateOfBirth,
                DateOfDeath= s.DateOfDeath,
                LastName = s.LastName,
                OtherName=s.OtherName,
                CountryId= s.CountryId,
                Country= s.Country
            }).First();
            return star;
        }
        public void Update(Star star)
        {
            _db.Update(star);
        }

        public void DeleteMovies(Star star, Movie movie, Function function)
        {
            var msf = _db.Movie_Stars_Functions.Where(x => x.MovieId == movie.MovieId && x.FunctionId == function.FunctionId && x.StarId == star.StarId).First();
            if(msf != null)
            {
                star.Movie_Stars_Functions.Remove(msf);
                movie.Movie_Stars_Functions.Remove(msf);
                function.Movie_Stars_Functions.Remove(msf);
                _db.Movie_Stars_Functions.Remove(msf);
                _db.SaveChanges();
            }
        }
    }
}
