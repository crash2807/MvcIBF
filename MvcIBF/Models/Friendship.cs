using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public ApplicationUser ApplicationUser1 { get; set; }
        public ApplicationUser ApplicationUser2 { get; set; }

    }
}
