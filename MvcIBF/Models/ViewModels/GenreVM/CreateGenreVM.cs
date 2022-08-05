using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.GenreVM
{
    public class CreateGenreVM
    {
        [Display(Name = "Gatunek")]
        [Required]

        public string? GenreName { get; set; }
    }
}
