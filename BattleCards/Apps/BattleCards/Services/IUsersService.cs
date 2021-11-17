namespace BattleCards.Services
{
    using BattleCards.ViewModels.Users;

    public interface IUsersService
    {
        void Register(UserRegisterInputModel registerInputModel);

        public string GetUserId(UserLoginInputModel loginInputModel);
    }
}
