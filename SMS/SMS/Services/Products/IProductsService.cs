namespace SMS.Services.Products
{
    using SMS.ViewModels.Products;
    using SMS.ViewModels.Users;

    public interface IProductsService
    {
        void CreateProduct(CreateProductInputModel model);

        UserLoggedInViewModel GetAll(string userId);
    }
}
