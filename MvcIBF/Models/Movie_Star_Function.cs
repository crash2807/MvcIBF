namespace MvcIBF.Models
{
    public class Movie_Star_Function
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int StarId { get; set; }
        public Star Star { get; set; }
        public int FunctionId { get; set; }
        public Function Function { get; set; }
    }
}
