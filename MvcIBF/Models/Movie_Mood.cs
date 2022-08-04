namespace MvcIBF.Models
{
    public class Movie_Mood
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int MoodId { get; set; }
        public Mood Mood { get; set; }  
    }
}
