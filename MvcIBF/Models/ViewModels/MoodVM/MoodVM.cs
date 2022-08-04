using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.MoodVM
{
    public class MoodVM
    {
        public int MoodId { get; set; }
        [Display(Name = "Nastrój")]
        [Required]

        public string? MoodName { get; set; }
        public IEnumerable<SelectListItem>? MoviesList { get; set; }
    }
}
