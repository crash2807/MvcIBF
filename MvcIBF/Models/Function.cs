using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    [Index(propertyNames: nameof(FunctionName), IsUnique = true)]
    public class Function
    {
        [Key]
        public int FunctionId { get; set; }
        [Display(Name = "Funkcja")]
        [Required]
        public string? FunctionName { get; set; }
        public ICollection<Movie_Star_Function> Movie_Stars_Functions { get; set; } = new List<Movie_Star_Function>();


    }
}
