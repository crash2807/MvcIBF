using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.MovieVM
{
    public class MovieFilterVM
    {
        [Display(Name = "Tytuł")]
        public string MovieTitle { get; set; }
        [Display(Name = "Data wydania od:")]
        public DateTime? ReleaseDateFrom { get; set; }
        [Display(Name = "Data wydania do:")]
        public DateTime? ReleaseDateTo { get; set; }
        [Display(Name = "Czas trwania")]
        public int? Runtime { get; set; }
        [Display(Name = "Opis")]
        public string MovieDescription { get; set; }
        [Display(Name = "Wybierz VOD")]
        public List<int>? SelectedVODs { get; set; }
        [Display(Name = "Wybierz VODy")]
        public MultiSelectList VODs { get;  set; }
        public List<int>? SelectedMoods { get; set; }
        [Display(Name = "Wybierz nastoje")]
        public MultiSelectList Moods { get; set; }
        public List<int>? SelectedCountries { get; set; }
        [Display(Name = "Wybierz kraje produkcji")]
        public MultiSelectList Countries { get; set; }
        public List<int>? SelectedGenres { get; set; }
        [Display(Name = "Wybierz nastoje")]
        public MultiSelectList Genres { get; set; }
        public List<Movie> FilteredMovies { get; set; }
    }
}
