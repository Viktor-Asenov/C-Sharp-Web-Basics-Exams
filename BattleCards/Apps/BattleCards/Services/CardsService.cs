namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext dbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<CardViewModel> GetAllCards()
        {
            var allCards = this.dbContext.Cards
                .Select(c => new CardViewModel 
                {
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    Type = c.Keyword,
                    Attack = c.Attack,
                    Health = c.Health
                })
                .ToList();

            return allCards;
        }

        public void AddNewCard(AddCardInputModel cardInputModel)
        {
            var cardToAdd = new Card
            {
                Name = cardInputModel.Name,
                ImageUrl = cardInputModel.ImageUrl,
                Keyword = cardInputModel.Keyword,
                Attack = cardInputModel.Attack,
                Health = cardInputModel.Health,
                Description = cardInputModel.Description
            };

            this.dbContext.Cards.Add(cardToAdd);
            this.dbContext.SaveChanges();
        }
    }
}
