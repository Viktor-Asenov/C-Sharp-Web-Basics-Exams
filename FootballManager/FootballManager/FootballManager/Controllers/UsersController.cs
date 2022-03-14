using FootballManager.Services.Users;
using FootballManager.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;

namespace FootballManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (model.Username == null || model.Password == null || model.Email == null)
            {
                return this.Redirect("/Users/Register");
            }

            this.userService.RegisterUser(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginInputModel model)
        {
            if (model.Username == null || model.Password == null)
            {
                return this.Redirect("/Users/Login");
            }

            var user = this.userService.GetUser(model);

            if (user == null)
            {
                return this.Error($"User with {model.Username} and password does not exist.");
            }

            this.SignIn(user.Id);

            return this.Redirect("/Players/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/Home/Index");
        }
    }
}
