using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class Movie_Friendship
    {
        
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
        public int FriendshipId { get; set; }
        public Friendship Friendship { get; set; }
        
        public string? Recommendation { get; set; }

    }
}
