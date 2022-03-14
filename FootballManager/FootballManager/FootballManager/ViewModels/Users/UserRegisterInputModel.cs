using System.ComponentModel.DataAnnotations;

namespace FootballManager.ViewModels.Users
{
    public class UserRegisterInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
