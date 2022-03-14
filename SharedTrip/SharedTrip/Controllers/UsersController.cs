using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Services.Users;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Controllers
{
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
        public HttpResponse Login(UserLoginInputModel model)
        {
            var user = this.usersService.GetUser(model);


            if (user == null)
            {
                return this.Error($"User with {model.Username} and password does not exist.");
            }

            this.SignIn(user.Id);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters long");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters long");
            }

            this.usersService.RegisterUser(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/Home/Index");
        }
    }
}
