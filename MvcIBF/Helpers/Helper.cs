using AutoMapper;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels;
using MvcIBF.Models.ViewModels.MovieVM;

namespace MvcIBF.Helpers
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<Movie, MovieVM>();
            CreateMap<MovieVM, Movie>();
            CreateMap<VOD, VODVM>();
            CreateMap<CreateVODVM, VOD>();
            CreateMap<VODVM, VOD>();
        }
    }
}
