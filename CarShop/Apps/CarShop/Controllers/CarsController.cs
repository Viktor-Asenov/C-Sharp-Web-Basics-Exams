namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.ViewModels.Cars;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IUsersService usersService;

        public CarsController(ICarService carService, IUsersService usersService)
        {
            this.carService = carService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            var allCarsViewModel = this.carService.AllCars();

            return this.View(allCarsViewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to add cars.");
            }

            var userId = this.GetUserId();
            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Error("User should not be mechanic in order to add cars.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCarInputModel carInputModel)
        {
            if (string.IsNullOrEmpty(carInputModel.Model))
            {
                return this.Error("Model should not be empty.");
            }

            if (carInputModel.Model.Length < 5)
            {
                return this.Error("Model should be between 5 and 20 characters long.");
            }

            if (carInputModel.Year <= 0)
            {
                return this.Error("Year should be positive number.");
            }

            if (string.IsNullOrEmpty(carInputModel.Image))
            {
                return this.Error("ImageUrl should not be empty.");
            }

            if (string.IsNullOrEmpty(carInputModel.PlateNumber))
            {
                return this.Error("Plate number should not be empty.");
            }

            var userId = this.GetUserId();
            if (this.usersService.IsUserMechanic(userId) == true)
            {
                return this.Error("Only clients can add cars.");
            }

            this.carService.AddCar(carInputModel, userId);

            return this.Redirect("/Cars/All");
        }
    }
}
