using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    [Index(propertyNames: nameof(CountryName), IsUnique = true)]
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Display(Name = "Kraj")]
        [Required]
        public string? CountryName { get; set; }
        public ICollection<Movie_Country>? Movie_Countries { get; set; }

    }
}
