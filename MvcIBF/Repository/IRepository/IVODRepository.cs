using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IVODRepository : IRepository<VOD>
    {
        void Update(VOD vod);
        List<VOD> GetAllMovieVods();
    }
}
