using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcIBF.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Display(Name = "Tytuł")]
        [Required]
        public string? MovieTitle { get; set; }

        [Display(Name ="Data premiery")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Czas trwania (min)")]
        public int Runtime { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string? MovieDescription { get; set; }
        public ICollection<Movie_VOD>? Movie_VODs { get; set; } = new List<Movie_VOD>();
        public ICollection<Movie_Mood>? Movie_Moods { get; set; } = new List<Movie_Mood>();

    }
}
