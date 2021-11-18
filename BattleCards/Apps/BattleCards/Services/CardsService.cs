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
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    Keyword = c.Keyword,
                    Attack = c.Attack,
                    Health = c.Health
                })
                .ToList();

            return allCards;
        }

        public IEnumerable<CardViewModel> GetAllUserCards(string userId)
        {
            var allUserCards = this.dbContext.UserCards
                .Where(uc => uc.UserId == userId)
                .Select(uc => new CardViewModel
                {
                    Id = uc.Card.Id,
                    Name = uc.Card.Name,
                    ImageUrl = uc.Card.ImageUrl,
                    Description = uc.Card.Description,
                    Keyword = uc.Card.Keyword,
                    Attack = uc.Card.Attack,
                    Health = uc.Card.Health
                })
                .ToList();

            return allUserCards;
        }

        public void AddNewCard(AddCardInputModel cardInputModel)
        {
            var cardToAdd = new Card
            {
                Name = cardInputModel.Name,
                ImageUrl = cardInputModel.Image,
                Keyword = cardInputModel.Keyword,
                Attack = cardInputModel.Attack,
                Health = cardInputModel.Health,
                Description = cardInputModel.Description
            };

            this.dbContext.Cards.Add(cardToAdd);
            this.dbContext.SaveChanges();
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            this.dbContext.UserCards.Add(new UserCard { UserId = userId, CardId = cardId });
            this.dbContext.SaveChanges();
        }

        public bool ContainsCard(string userId, int cardId)
        {
            return this.dbContext.UserCards
                .Any(uc => uc.UserId == userId && uc.CardId == cardId);
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var userCardToRemove = this.dbContext.UserCards
                .FirstOrDefault(uc => uc.UserId == userId && uc.CardId == cardId);

            this.dbContext.UserCards.Remove(userCardToRemove);
            this.dbContext.SaveChanges();    
        }
    }
}
