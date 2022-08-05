using AutoMapper;
using MvcIBF.Models;
using MvcIBF.Models.ViewModels;
using MvcIBF.Models.ViewModels.CountryVM;
using MvcIBF.Models.ViewModels.GenreVM;
using MvcIBF.Models.ViewModels.MoodVM;
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
            CreateMap<Mood, MoodVM>();
            CreateMap<MoodVM, Mood>();
            CreateMap<CreateMoodVM, Mood>();
            CreateMap<Genre, GenreVM>();
            CreateMap<GenreVM, Genre>();
            CreateMap<CreateGenreVM, Genre>();
            CreateMap<Country, CountryVM>();
            CreateMap<CountryVM, Country>();
            CreateMap<CreateCountryVM, Country>();
        }
    }
}
