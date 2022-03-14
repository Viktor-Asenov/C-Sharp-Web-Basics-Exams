namespace SMS.Services.Users
{
    using SMS.Models;
    using SMS.ViewModels.Users;

    public interface IUserService
    {
        void RegisterUser(UserRegisterInputModel model);

        User GetUser(UserLoginInputModel model);
    }
}
