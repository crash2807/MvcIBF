using System.ComponentModel.DataAnnotations;
namespace MvcIBF.Models
{
    public class VOD
    {
        [Key]
        public int VodId { get; set; }
        [Display(Name = "VOD")]
        [Required]
        public string? VodName { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}
