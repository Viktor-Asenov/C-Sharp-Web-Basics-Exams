namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Services.Products;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("Home/IndexLoggedIn");
            }

            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            var userId = this.User.Id;
            var allProuctsViewModel = this.productsService.GetAll(userId);

            return this.View(allProuctsViewModel);
        }
    }
}