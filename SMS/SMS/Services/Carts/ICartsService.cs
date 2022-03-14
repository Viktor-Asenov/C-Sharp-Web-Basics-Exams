namespace SMS.Services.Carts
{
    using SMS.ViewModels.Carts;
    using System.Collections.Generic;

    public interface ICartsService
    {
        void AddProductIntoCart(string productId, string userId);

        IEnumerable<CartProductsViewModel> GetAll(string userId);
    }
}
