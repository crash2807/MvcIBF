using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.FunctionVM
{
    public class CreateFunctionVM
    {
        [Display(Name = "Funkcja filmowa")]
        [Required]

        public string? FunctionName { get; set; }
    }
}
