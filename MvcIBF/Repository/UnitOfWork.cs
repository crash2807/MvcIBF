using MvcIBF.Data;
using MvcIBF.Repository.IRepository;
using MvcIBF.Repository;

namespace MvcIBF.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MvcIBFContext _db;
        public UnitOfWork(MvcIBFContext db)
        {
            _db = db;
            VOD = new VODRepository(_db);
            Movie = new MovieRepository(_db);
            Mood = new MoodRepository(_db);
            Genre = new GenreRepository(_db);
            Country = new CountryRepository(_db);
            Material = new MaterialRepository(_db);
            Star = new StarRepository(_db);
            Function = new FunctionRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Rating = new RatingRepository(_db);
            Friendship = new FriendshipRepository(_db);
        }
        public IVODRepository VOD { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IMoodRepository Mood { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IMaterialRepository Material { get; private set; }
        public IStarRepository Star { get; private set; }
        public IFunctionRepository Function { get; private set; }
        public IRatingRepository Rating { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IFriendshipRepository Friendship {get; private set; }

        public void Save()
        {
            _db.SaveChanges(); 
        }
    } 
}
