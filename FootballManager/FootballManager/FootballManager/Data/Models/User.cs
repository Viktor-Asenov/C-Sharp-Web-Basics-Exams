using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserPlayers = new HashSet<UserPlayer>();
        }

        public string Id { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        public ICollection<UserPlayer> UserPlayers { get; set; }
    }
}
