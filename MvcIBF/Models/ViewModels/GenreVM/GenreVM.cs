using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.GenreVM
{
    public class GenreVM
    {
        public int GenreId { get; set; }
        [Display(Name = "Gatunek")]
        [Required]

        public string? GenreName { get; set; }
        public IEnumerable<SelectListItem>? MoviesList { get; set; }
    }
}
