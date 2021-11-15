using Git.Services;
using Git.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
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
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Error("Username or password is invalid!");
            }

            this.SignIn(userId);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if (inputModel == null)
            {
                return this.Error("All fields must be populated.");
            }

            if (string.IsNullOrEmpty(inputModel.Username) || string.IsNullOrWhiteSpace(inputModel.Username))
            {
                return this.Error("Username is not valid.");
            }

            if (inputModel.Username.Length < 5)
            {
                return this.Error("Lenght should be at least 6 characters long.");
            }

            if (string.IsNullOrEmpty(inputModel.Email) || string.IsNullOrWhiteSpace(inputModel.Email))
            {
                return this.Error("Email is not valid.");
            }

            if (string.IsNullOrEmpty(inputModel.Password) || string.IsNullOrWhiteSpace(inputModel.Password))
            {
                return this.Error("Password is not valid.");
            }

            if (inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.Error("Incorrect password lenght.");
            }

            this.usersService.CreateUser(inputModel.Username, inputModel.Email, inputModel.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/Repositories/All");
        }
    }
}
