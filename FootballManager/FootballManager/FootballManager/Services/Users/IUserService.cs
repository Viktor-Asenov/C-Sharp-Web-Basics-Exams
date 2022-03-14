using FootballManager.Data.Models;
using FootballManager.ViewModels.Users;

namespace FootballManager.Services.Users
{
    public interface IUserService
    {
        void RegisterUser(UserRegisterInputModel model);

        User GetUser(UserLoginInputModel model);
    }
}
