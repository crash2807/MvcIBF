using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
        [Required]
        public string URL { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
