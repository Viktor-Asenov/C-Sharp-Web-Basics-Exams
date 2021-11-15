using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string id)
        {
            var viewModel = this.commitsService.GetCommitRepositoryModel(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string id, string creatorId, string description)
        {
            if (description.Length < 5)
            {
                return this.Error("Description should be at least 5 characters.");
            }

            id = this.Request.QueryString.Replace("id=", string.Empty);
            creatorId = this.GetUserId();

            this.commitsService.Create(id, creatorId, description);
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be signed in order to see commits.");
            }

            var commitsViewModel = this.commitsService.GetAllCommits();
            return this.View(commitsViewModel);
        }

        public HttpResponse Delete() 
        {
            string creatorId = this.GetUserId();

            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to delete commit.");
            }

            if (string.IsNullOrEmpty(creatorId))
            {
                return this.Error("User should be registered in order to delete commits.");
            }

            this.commitsService.Delete(creatorId);

            return this.Redirect("/Commits/All");
        }
    }
}
