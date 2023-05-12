using System.ComponentModel.DataAnnotations;

namespace MvcIBF.Models.ViewModels.FriendshipVM
{
    public class FriendshipVM
    {
        public int FriendshipId { get; set; }
        [Display(Name = "Zdjęcie profilowe")]
        public string ProfilePictureURL { get; set; }
        //public string UserName { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string Email { get; set; }
        public string User2Id { get; set; }
    }
}
