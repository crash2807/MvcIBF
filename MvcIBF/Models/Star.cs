using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class Star
    {
        [Key]
        public int StarId { get; set; }
        [Required]
        [Display(Name = "Imię")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name ="Nazwisko")]
        public string? LastName { get; set; }
        [Display(Name ="Inny pseudonim")]
        public string? OtherName { get; set; }
        [Display(Name ="Data urodzenia")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Data śmierci")]
        [DataType(DataType.Date)]
        public DateTime? DateOfDeath { get; set; }
        public string? ProfilePictureURL { get; set; }
        [Display(Name ="Kraj pochodzenia")]
        public Country Country { get; set; }
        public int CountryId { get; set; }
        [Display(Name = "Filmografia")]
        public ICollection<Movie_Star_Function> Movie_Stars_Functions { get; set; } = new List<Movie_Star_Function>();

    }
}
