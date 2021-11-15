using Git.Data;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        ICollection<RepositoryViewModel> GetAllRepositories(string userId);

        void Create(string name, string ownerId);
    }
}
