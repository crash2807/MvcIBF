using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string? ProfilePictureUrl { get; set; }
    }
}
