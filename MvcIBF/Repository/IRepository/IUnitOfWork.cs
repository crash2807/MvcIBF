namespace MvcIBF.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IVODRepository VOD { get; }
        IMovieRepository Movie { get; }
        IMoodRepository Mood { get; }
        IGenreRepository Genre { get; }
        ICountryRepository Country { get; }
        IMaterialRepository Material { get; }
        IFunctionRepository Function { get; }
        IStarRepository Star { get; }
        void Save();
    }
}
