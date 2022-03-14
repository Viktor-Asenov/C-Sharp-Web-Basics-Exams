using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.ViewModels.Players;
using System;
using System.Linq;

namespace FootballManager.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly FootballManagerDbContext dbContext;

        public PlayerService(FootballManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Player AddPlayer(AddPlayerInputModel model)
        {
            var player = new Player
            {
                FullName = model.FullName,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed,
                Endurance = model.Endurance,
                Description = model.Description,
            };

            this.dbContext.Players.Add(player);

            this.dbContext.SaveChanges();

            return player;
        }

        public void AddPlayerToUserCollectionWhenCreated(string userId, Player player)
        {
            var user = this.dbContext.Users
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return;
            }

            var userPlayer = new UserPlayer
            {
                PlayerId = player.Id,
                UserId = user.Id,
            };

            this.dbContext.UserPlayers.Add(userPlayer);
            this.dbContext.SaveChanges();
        }

        public AllPlayersViewModel GetAll()
        {
            var allPlayers = new AllPlayersViewModel
            {
                Players = this.dbContext.Players
                    .Select(p => new PlayerViewModel
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Position = p.Position,
                        Speed = p.Speed.ToString(),
                        Endurance = p.Endurance.ToString()
                    })
                    .ToList()
            };

            return allPlayers;
        }

        public AllPlayersViewModel GetAllOfTheUser(string userId)
        {
            var allUserPlayers = new AllPlayersViewModel
            {
                Players = this.dbContext.UserPlayers
                    .Where(up => up.UserId == userId)
                    .Select(up => new PlayerViewModel
                    {
                        Id = up.Player.Id,
                        FullName = up.Player.FullName,
                        Description = up.Player.Description,
                        ImageUrl = up.Player.ImageUrl,
                        Position = up.Player.Position,
                        Speed = up.Player.Speed.ToString(),
                        Endurance = up.Player.Endurance.ToString()
                    })
                    .ToList()
            };

            return allUserPlayers;
        }

        public void AddPlayerToUserCollection(string userId, int playerId)
        {
            var userPlayer = this.dbContext.UserPlayers
                .FirstOrDefault(up => up.UserId == userId && up.PlayerId == playerId);

            if (userPlayer != null)
            {
                return;
            }

            userPlayer = new UserPlayer
            {
                UserId = userId,
                PlayerId = playerId,
            };

            this.dbContext.UserPlayers.Add(userPlayer);
            this.dbContext.SaveChanges();
        }

        public void RemovePlayerFromUserCollection(string userId, int playerId)
        {
            var userPlayer = this.dbContext.UserPlayers
                .FirstOrDefault(up => up.UserId == userId && up.PlayerId == playerId);

            if (userPlayer != null)
            {
                this.dbContext.UserPlayers.Remove(userPlayer);
                this.dbContext.SaveChanges();
            }
        }
    }
}
