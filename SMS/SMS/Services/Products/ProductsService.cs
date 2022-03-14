namespace SMS.Services.Products
{
    using SMS.Data;
    using SMS.Models;
    using SMS.ViewModels.Products;
    using SMS.ViewModels.Users;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly SMSDbContext dbContext;

        public ProductsService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateProduct(CreateProductInputModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
            };

            this.dbContext.Products.Add(product);
            this.dbContext.SaveChanges();
        }

        public UserLoggedInViewModel GetAll(string userId)
        {
            var allProductsWithUser = this.dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserLoggedInViewModel
                {
                    Username = u.Username,
                    Products = this.dbContext.Products
                        .Select(p => new ProductViewModel
                        {
                            Id = p.Id,
                            ProductName = p.Name,
                            Price = p.Price,
                        })
                        .OrderByDescending(p => p.Price)
                        .ToList()
                })
                .FirstOrDefault();

            return allProductsWithUser;
        }
    }
}
