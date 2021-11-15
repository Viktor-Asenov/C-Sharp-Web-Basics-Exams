using Git.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = ComputeHash(password)
            };

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var passwordHashed = ComputeHash(password);
            var user = this.dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == passwordHashed);

            return user.Id == null ? null : user.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return this.dbContext.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return this.dbContext.Users.Any(u => u.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
