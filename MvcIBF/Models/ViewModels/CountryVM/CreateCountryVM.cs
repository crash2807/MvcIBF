using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.CountryVM
{
    public class CreateCountryVM
    {
        [Display(Name = "Kraj")]
        [Required]
        public string? CountryName { get; set; }
    }
}
