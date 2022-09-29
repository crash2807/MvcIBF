using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.StarVM
{
    public class StarVM
    {
        public int StarId { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name = "Nazwisko")]
        public string? LastName { get; set; }
        [Display(Name = "Inny pseudonim")]
        public string? OtherName { get; set; }
        [Display(Name = "Data urodzenia")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Data śmierci")]
        [DataType(DataType.Date)]
        public DateTime? DateOfDeath { get; set; }
        [Display(Name = "Zdjęcie profilowe")]
        public string? ProfilePictureURL { get; set; }
        [Display(Name = "Kraj pochodzenia")]
        public string? CountryName { get; set; }
        [Display(Name = "Kraj pochodzenia")]
        public IEnumerable<SelectListItem>? CountriesList { get; set; }
        public int? SelectedCountry { get; set; }
    }
}
