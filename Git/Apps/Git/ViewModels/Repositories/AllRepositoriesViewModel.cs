using System.Collections.Generic;

namespace Git.ViewModels.Repositories
{
    public class AllRepositoriesViewModel
    {
        public ICollection<RepositoryViewModel> Repositories { get; set; }
    }
}
