using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels
{
    public class VODVM
    {
        public int VodId { get; set; }
        [Display(Name = "VOD")]
        [Required]
        public string? VodName { get; set; }
        public IEnumerable<SelectListItem>? MoviesList { get; set; }
    }
}
