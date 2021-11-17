namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to see all cards.");
            }

            var allCardsModel = this.cardsService.GetAllCards();

            return this.View(allCardsModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to add card.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddCardInputModel cardInputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to add card.");
            }

            this.cardsService.AddNewCard(cardInputModel);

            return this.Redirect("/Cards/All");
        }
    }
}
