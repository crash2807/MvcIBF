namespace MvcIBF.Models
{
    public class Movie_VOD
    {
        
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int VODId { get; set; }
        public VOD VOD { get; set; }
    }
}
