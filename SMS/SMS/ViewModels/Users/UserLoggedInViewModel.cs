namespace SMS.ViewModels.Users
{
    using SMS.ViewModels.Products;
    using System.Collections.Generic;

    public class UserLoggedInViewModel
    {
        public string Username { get; set; }

        public ICollection<ProductViewModel> Products { get; set; }
    }
}
