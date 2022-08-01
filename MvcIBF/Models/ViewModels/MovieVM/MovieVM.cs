using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.MovieVM
{
    public class MovieVM
    {
        public int MovieId { get; set; }
        [Display(Name = "Tytuł")]
        [Required]

        public string? MovieTitle { get; set; }
        [Display(Name = "Data premiery")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Czas trwania (min)")]
        public int Runtime { get; set; }
        [Display(Name = "Opis")]
        [Required]
        public string? MovieDescription { get; set; }

        public IEnumerable<SelectListItem>? VODsList { get; set; }

        public int[]? SelectedVODs { get; set; }

        public List<string>? VodNames { get; set; }
    }
}
