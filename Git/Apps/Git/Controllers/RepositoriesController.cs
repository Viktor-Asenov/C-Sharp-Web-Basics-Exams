using Git.Data;
using Git.Services;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            var repositoriesModel = this.repositoriesService.GetAllRepositories(this.GetUserId());
            
            return this.View(repositoriesModel);
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be loged in order to create repositories.");
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return this.Error("Name should not be empty.");
            }

            if (name.Length < 3)
            {
                return this.Error("Name should be at least 3 characters long.");
            }

            this.repositoriesService.Create(name, this.GetUserId());

            return this.Redirect("/Repositories/All");
        }
    }
}
