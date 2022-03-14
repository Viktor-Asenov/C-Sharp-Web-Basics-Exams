namespace SMS.Services.Carts
{
    using SMS.Data;
    using SMS.ViewModels.Carts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CartsService : ICartsService
    {
        private readonly SMSDbContext dbContext;

        public CartsService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddProductIntoCart(string productId, string userId)
        {
            var product = this.dbContext.Products
                .FirstOrDefault(p => p.Id == productId);

            var user = this.dbContext.Users
                .FirstOrDefault(u => u.Id == userId);

            if (product == null)
            {
                throw new NullReferenceException("Product does not exist.");
            }

            if (user == null)
            {
                throw new NullReferenceException("User does not exist.");
            }

            user.Cart = this.dbContext.Carts
                .FirstOrDefault(c => c.User == user);

            user.Cart.Products.Add(product);
            product.CartId = user.CartId;

            this.dbContext.SaveChanges();
        }

        public IEnumerable<CartProductsViewModel> GetAll(string userId)
        {
            var cartProducts = this.dbContext.Products
                .Where(p => p.Cart.User.Id == userId)
                .Select(p => new CartProductsViewModel
                {
                    Name = p.Name,
                    Price = p.Price,
                })
                .OrderByDescending(p => p.Price)
                .ToList();

            return cartProducts;
        }
    }
}
