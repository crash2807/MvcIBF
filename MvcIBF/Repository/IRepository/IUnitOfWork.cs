namespace MvcIBF.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IVODRepository VOD { get; }
        IMovieRepository Movie { get; }
        void Save();
    }
}
