using System.ComponentModel.DataAnnotations;

namespace FootballManager.ViewModels.Users
{
    public class UserLoginInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
