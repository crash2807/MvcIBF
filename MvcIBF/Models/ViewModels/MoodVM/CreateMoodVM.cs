using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.MoodVM
{
    public class CreateMoodVM
    {
        [Display(Name = "Nastrój")]
        [Required]

        public string? MoodName { get; set; }
    }
}
