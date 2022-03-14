namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Services.Products;
    using SMS.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateProductInputModel model)
        {
            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Name should be between 4 and 20 characters long.");
            }

            this.productsService.CreateProduct(model);

            return this.Redirect("/Home/IndexLoggedIn");
        }
    }
}
