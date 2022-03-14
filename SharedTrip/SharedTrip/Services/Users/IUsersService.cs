using SharedTrip.Models;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services.Users
{
    public interface IUsersService
    {
        void RegisterUser(UserRegisterInputModel model);

        User GetUser(UserLoginInputModel model);
    }
}
