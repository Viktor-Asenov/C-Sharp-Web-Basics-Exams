using FootballManager.Data.Models;
using FootballManager.ViewModels.Players;

namespace FootballManager.Services.Players
{
    public interface IPlayerService
    {
        Player AddPlayer(AddPlayerInputModel model);

        void AddPlayerToUserCollectionWhenCreated(string userId, Player player);

        AllPlayersViewModel GetAll();

        AllPlayersViewModel GetAllOfTheUser(string userId);

        void AddPlayerToUserCollection(string userId, int playerId);

        void RemovePlayerFromUserCollection(string userId, int playerId);
    }
}
