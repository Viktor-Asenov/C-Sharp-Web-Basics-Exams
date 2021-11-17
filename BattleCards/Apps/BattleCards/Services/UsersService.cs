namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Users;
    using System.Linq;
    using System.Text;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Register(UserRegisterInputModel registerInputModel)
        {
            var user = new User
            {
                Username = registerInputModel.Username,
                Email = registerInputModel.Email,
                Password = ComputeHash(registerInputModel.Password)
            };

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }

        public string GetUserId(UserLoginInputModel loginInputModel)
        {
            var passwordHashed = ComputeHash(loginInputModel.Password);
            var user = this.dbContext.Users.FirstOrDefault(u => u.Username == loginInputModel.Username
                && u.Password == passwordHashed);

            return user.Id == null ? null : user.Id;
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
