using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    [Index(propertyNames: nameof(MoodName), IsUnique = true)]
    public class Mood
    {
        [Key]
        public int MoodId { get; set; }
        [Display(Name = "Nastrój")]
        [Required]
        public string? MoodName { get; set; }
        public ICollection<Movie_Mood>? Movie_Moods { get; set; }

    }
}
