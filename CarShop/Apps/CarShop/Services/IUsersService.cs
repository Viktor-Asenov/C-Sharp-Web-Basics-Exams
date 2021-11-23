namespace CarShop.Services
{
    using CarShop.ViewModels.Users;

    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void Create(RegisterInputModel inputModel);

        bool IsUsernameAvailable(string username);

        public bool IsUserMechanic(string userId);
    }
}
