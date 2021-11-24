namespace CarShop.Controllers
{
    using CarShop.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class IssuesController : Controller
    {
        private readonly IIssueService issueService;
        private readonly IUsersService usersService;

        public IssuesController(IIssueService issueService, IUsersService usersService)
        {
            this.issueService = issueService;
            this.usersService = usersService;
        }

        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to check issues.");
            }

            var carIssuesViewModel = this.issueService.GetAllCarIssuesViewModel(carId);

            return this.View(carIssuesViewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to add issues.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return this.Error("Description should not be empty.");
            }

            if (description.Length < 5)
            {
                return this.Error("Description should be at least 5 characters long.");
            }

            string carId = this.Request.QueryString.Replace("CarId=", string.Empty); 
            this.issueService.AddIssue(carId, description);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Fix(string issueId, string carId)
        {
            var userId = this.GetUserId();
            if (!this.usersService.IsUserMechanic(userId))
            {
                return this.Error("User should be mechanic in order to fix issues.");
            }

            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to fix issues.");
            }

            this.issueService.FixIssue(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to delete issues.");
            }

            this.issueService.DeleteIssue(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
