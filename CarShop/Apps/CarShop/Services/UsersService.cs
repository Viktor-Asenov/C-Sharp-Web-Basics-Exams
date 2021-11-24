namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Users;
    using System.Linq;
    using System.Text;

    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(RegisterInputModel inputModel)
        {
            var user = new User
            {
                Username = inputModel.Username,
                Email = inputModel.Email,
                Password = ComputeHash(inputModel.Password),
                IsMechanic = inputModel.UserType == "Mechanic" ? true : false
            };

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var computedPassword = ComputeHash(password);
            var user = this.dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == computedPassword);

            return user.Id;
        }

        public bool IsUserMechanic(string userId)
        {
            var user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId);

            return user.IsMechanic;
        }

        public bool IsUsernameAvailable(string username)
        {
            if (this.dbContext.Users.FirstOrDefault(u => u.Username == username) != null)
            {
                return false;
            }

            return true;
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
