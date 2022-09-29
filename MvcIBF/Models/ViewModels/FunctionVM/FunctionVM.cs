using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.FunctionVM
{
    public class FunctionVM
    {
        public int FunctionId { get; set; }
        [Display(Name = "Funkcja filmowa")]
        [Required]
        public string? FunctionName { get; set; }
        //public IEnumerable<SelectListItem>? MoviesList { get; set; }
    }
}
