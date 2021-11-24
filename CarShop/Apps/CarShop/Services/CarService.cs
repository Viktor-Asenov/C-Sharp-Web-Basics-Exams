namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly ApplicationDbContext dbContext;

        public CarService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<CarViewModel> AllCars()
        {
            var allCars = this.dbContext.Cars
                .Select(c => new CarViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    PlateNumber = c.PlateNumber,
                    PictureUrl = c.PictureUrl,
                    RemainingIssues = c.Issues.Where(i => i.IsFixed == false).Count(),
                    FixedIssues = c.Issues.Where(i => i.IsFixed == true).Count()
                })
                .ToList();

            return allCars;
        }

        public void AddCar(AddCarInputModel carInputModel, string ownerId)
        {
            var car = new Car
            {
                Model = carInputModel.Model,
                Year = carInputModel.Year,
                PictureUrl = carInputModel.Image,
                PlateNumber = carInputModel.PlateNumber,
                OwnerId = ownerId
            };

            this.dbContext.Cars.Add(car);
            this.dbContext.SaveChanges();
        }
    }
}
