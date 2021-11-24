namespace CarShop.Services
{
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarViewModel> AllCars();

        void AddCar(AddCarInputModel carInputModel, string ownerId);
    }
}
