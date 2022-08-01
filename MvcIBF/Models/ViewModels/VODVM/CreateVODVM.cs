using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels
{
    public class CreateVODVM
    {
        [Display(Name = "VOD")]
        [Required]

        public string? VodName { get; set; }
    }
}
