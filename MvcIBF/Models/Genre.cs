using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    [Index(propertyNames: nameof(GenreName), IsUnique = true)]

    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public string? GenreName { get; set; }
        public ICollection<Movie_Genre>? Movie_Genres { get; set; }
    }
}
