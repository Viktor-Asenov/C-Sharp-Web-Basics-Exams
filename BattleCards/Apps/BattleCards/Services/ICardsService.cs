namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardViewModel> GetAllCards();

        IEnumerable<CardViewModel> GetAllUserCards(string userId);

        void AddNewCard(AddCardInputModel cardInputModel);

        void AddCardToUserCollection(string userId, int cardId);

        bool ContainsCard(string userId, int cardId);

        void RemoveCardFromUserCollection(string userId, int cardId);
    }
}
