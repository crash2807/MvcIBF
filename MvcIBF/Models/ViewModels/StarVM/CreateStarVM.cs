using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.StarVM
{
    public class CreateStarVM
    {
       
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
        public string? ProfilePictureURL { get; set; }
        [Display(Name = "Kraj pochodzenia")]
        public IEnumerable<SelectListItem>? CountriesList { get; set; }
        public int SelectedCountry { get; set; }
        //[Display(Name = "Kraj pochodzenia")]

        //public string CountryName { get; set; }
        
    }
}
