using FootballManager.Services.Players;
using FootballManager.ViewModels.Players;
using MyWebServer.Controllers;
using MyWebServer.Http;

namespace FootballManager.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        public HttpResponse All()
        {
            var allPlayersModel = this.playerService.GetAll();

            return this.View(allPlayersModel);
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddPlayerInputModel model)
        {
            var userId = this.User.Id;
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            var addedPlayer = this.playerService.AddPlayer(model);
            this.playerService.AddPlayerToUserCollectionWhenCreated(userId, addedPlayer);

            return this.Redirect("/Players/All");
        }

        public HttpResponse Collection()
        {
            var userId = this.User.Id;
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            var allUserPlayersModel = this.playerService.GetAllOfTheUser(userId);

            return this.View(allUserPlayersModel);
        }

        public HttpResponse AddToCollection(int playerId)
        {
            var userId = this.User.Id;
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.playerService.AddPlayerToUserCollection(userId, playerId);

            return this.Redirect("/Players/All");
        }

        public HttpResponse RemoveFromCollection(int playerId)
        {
            var userId = this.User.Id;
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.playerService.RemovePlayerFromUserCollection(userId, playerId);

            return this.Redirect("/Players/Collection");
        }
    }
}
