namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardViewModel> GetAllCards();

        void AddNewCard(AddCardInputModel cardInputModel);
    }
}
