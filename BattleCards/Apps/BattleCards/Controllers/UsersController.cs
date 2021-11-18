namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using BattleCards.ViewModels.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginInputModel loginInputModel)
        {
            if (string.IsNullOrEmpty(loginInputModel.Username))
            {
                return this.Error("Username should not be empty.");
            }

            if (string.IsNullOrEmpty(loginInputModel.Password))
            {
                return this.Error("Password should not be empty.");
            }

            var userId = this.usersService.GetUserId(loginInputModel);

            this.SignIn(userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel registerInputModel)
        {
            if (string.IsNullOrEmpty(registerInputModel.Username))
            {
                return this.Error("Username should not be empty.");
            }

            if (registerInputModel.Username.Length < 5 || registerInputModel.Username.Length > 20)
            {
                return this.Error("Username lenght should be between 5 and 20 characters.");
            }

            if (string.IsNullOrEmpty(registerInputModel.Email))
            {
                return this.Error("Email should not be empty.");
            }

            if (string.IsNullOrEmpty(registerInputModel.Password))
            {
                return this.Error("Password should not be empty.");
            }

            if (registerInputModel.Password.Length < 6 || registerInputModel.Password.Length > 20)
            {
                return this.Error("Password lenght should be between 6 and 20 characters.");
            }

            this.usersService.Register(registerInputModel);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
