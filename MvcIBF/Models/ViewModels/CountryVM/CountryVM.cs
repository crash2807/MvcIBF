using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.CountryVM
{
    public class CountryVM
    {
        public int CountryId { get; set; }
        [Display(Name = "Kraj")]
        [Required]
        public string? CountryName { get; set; }
        public IEnumerable<SelectListItem>? MoviesList { get; set; }
    }
}
