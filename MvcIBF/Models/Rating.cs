using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class Rating
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Ocena")]
        [Required]
        public int RatingValue { get; set; }
        [Display(Name = "Komentarz")]
        public string? Comment { get; set; }
        
    }
}
