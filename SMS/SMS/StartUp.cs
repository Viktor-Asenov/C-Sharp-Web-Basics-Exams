namespace SMS
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using SMS.Data;
    using SMS.Services.Carts;
    using SMS.Services.Products;
    using SMS.Services.Users;

    public class StartUp
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IUserService, UserService>()
                    .Add<IProductsService, ProductsService>()
                    .Add<ICartsService, CartsService>()
                    .Add<SMSDbContext>())
                .WithConfiguration<SMSDbContext>(c => c.Database.Migrate())
                .Start();
    }
}