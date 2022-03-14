namespace SMS.Services.Users
{
    using SMS.Data;
    using SMS.Models;
    using SMS.ViewModels.Users;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly SMSDbContext dbContext;

        public UserService(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void RegisterUser(UserRegisterInputModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = ComputeHash(model.Password),
            };

            var cart = new Cart { };
            user.Cart = cart;

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }

        public User GetUser(UserLoginInputModel model)
        {
            var hashedPassword = ComputeHash(model.Password);
            var user = this.dbContext.Users
                .FirstOrDefault(u => u.Username == model.Username
                && u.Password == hashedPassword);

            return user;
        }

        private static string ComputeHash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
