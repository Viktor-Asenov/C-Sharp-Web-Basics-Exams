using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Services.Trips;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Home/Index");
            }

            var tripsModel = this.tripsService.GetAll();

            return this.View(tripsModel);
        }

        public HttpResponse Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Error("Logged users only can access this page.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel model)
        {
            if (model == null)
            {
                return this.View();
            }

            this.tripsService.AddTrip(model);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (tripId == null)
            {
                return this.View();
            }

            if (!this.User.IsAuthenticated)
            {
                return this.Error("Logged users only can access this page.");
            }

            var tripDetailsModel = this.tripsService.GetTrip(tripId);

            return this.View(tripDetailsModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (tripId == null)
            {
                return this.View();
            }

            if (!this.User.IsAuthenticated)
            {
                return this.Error("Logged users only can access this page.");
            }

            var userId = this.User.Id;
            var result = this.tripsService.AddUserToTrip(tripId, userId);

            if (result != "Success!")
            {
                return this.Redirect("/Trips/Details?tripId=" + tripId);
            }            

            return this.Redirect("/Trips/All");
        }
    }
}
