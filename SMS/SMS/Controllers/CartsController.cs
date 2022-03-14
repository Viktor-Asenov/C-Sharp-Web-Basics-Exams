namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Services.Carts;

    public class CartsController : Controller
    {
        private readonly ICartsService cartsService;

        public CartsController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        public HttpResponse AddProduct(string productId)
        {
            var userId = this.User.Id;
            this.cartsService.AddProductIntoCart(productId, userId);

            return this.Redirect("/Carts/Details");
        }

        public HttpResponse Details()
        {
            var userId = this.User.Id;
            var productsInCartViewModel = this.cartsService.GetAll(userId);

            return this.View(productsInCartViewModel);
        }

        public HttpResponse Buy()
        {
            return this.Redirect("/Home/IndexLoggedIn");
        }
    }
}
