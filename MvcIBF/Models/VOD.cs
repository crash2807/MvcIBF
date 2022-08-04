using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MvcIBF.Models
{
    [Index(propertyNames: nameof(VodName), IsUnique = true)]
    public class VOD
    {
        [Key]
        public int VodId { get; set; }
        
        [Display(Name = "VOD")]
        [Required]
       

        public string? VodName { get; set; }
        public ICollection<Movie_VOD>? Movie_VODs { get; set; }
    }
}
