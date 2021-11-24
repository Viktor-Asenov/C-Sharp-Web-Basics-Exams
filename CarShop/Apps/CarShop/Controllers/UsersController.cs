namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.ViewModels.Users;
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
        public HttpResponse Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return this.Error("Username should not be empty.");
            }

            if (string.IsNullOrEmpty(password))
            {
                return this.Error("Password should not be empty.");
            }

            var userId = this.usersService.GetUserId(username, password);

            this.SignIn(userId);

            return this.Redirect("/Cars/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if (!this.usersService.IsUsernameAvailable(inputModel.Username))
            {
                return this.Error("Username is already taken.");
            }

            if (string.IsNullOrEmpty(inputModel.Username))
            {
                return this.Error("Username should not be empty.");
            }

            if (inputModel.Username.Length < 4)
            {
                return this.Error("Username should be between 4 and 20 characters long.");
            }

            if (string.IsNullOrEmpty(inputModel.Email))
            {
                return this.Error("Email should not be empty.");
            }

            if (string.IsNullOrEmpty(inputModel.Password))
            {
                return this.Error("Password should not be empty.");
            }

            if (inputModel.Password.Length < 5 || inputModel.Password.Length > 20)
            {
                return this.Error("Password should be between 5 and 20 characters long.");
            }

            this.usersService.Create(inputModel);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
