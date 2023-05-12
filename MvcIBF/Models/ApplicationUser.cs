using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Zdjęcie profilowe")]
        public string? ProfilePictureUrl { get; set; }
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();

    }
}
