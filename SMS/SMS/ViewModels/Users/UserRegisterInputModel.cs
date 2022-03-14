namespace SMS.ViewModels.Users
{
    using SMS.Models;

    public class UserRegisterInputModel
    {
        public string Username { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }

        public Cart Cart { get; set; }
    }
}
