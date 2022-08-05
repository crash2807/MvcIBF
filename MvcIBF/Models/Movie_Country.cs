namespace MvcIBF.Models
{
    public class Movie_Country
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}