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

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to see his cards.");
            }

            var userId = this.GetUserId();
            var allUserCardsViewModel = this.cardsService.GetAllUserCards(userId);

            return this.View(allUserCardsViewModel);
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

            if (cardInputModel.Name.Length < 5)
            {
                return this.Error("Name should be at least 5 characters long.");
            }

            this.cardsService.AddNewCard(cardInputModel);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to add card to his collection.");
            }

            var userId = this.GetUserId();
            if (this.cardsService.ContainsCard(userId, cardId))
            {
                return this.Redirect("/Cards/All");
            }

            this.cardsService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("User should be logged in order to add card to his collection.");
            }

            var userId = this.GetUserId();
            this.cardsService.RemoveCardFromUserCollection(userId, cardId);

            return this.Redirect("/Cards/Collection");
        }
    }
}
