using Git.Data;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext dbContext;

        public RepositoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string name, string ownerId)
        {
            var repository = new Repository
            {
                Name = name,
                OwnerId = ownerId,
                CreatedOn = DateTime.UtcNow
            };

            this.dbContext.Repositories.Add(repository);
            this.dbContext.SaveChanges();
        }

        public ICollection<RepositoryViewModel> GetAllRepositories(string userId)
        {
            var repositories = this.dbContext.Repositories
                .Where(r => r.OwnerId == userId)
                .Select(r => new RepositoryViewModel 
                {
                    Id = r.Id,
                    Name = r.Name,
                    OwnerUserName = r.Owner.Username,
                    CreatedOn = r.CreatedOn,
                    CommitsCount = r.Commits.Count
                })
                .ToList();

            return repositories;
        }
    }
}
