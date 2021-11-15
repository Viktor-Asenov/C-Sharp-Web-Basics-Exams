using Git.Data;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext dbContext;

        public CommitsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string id, string creatorId, string description)
        {
            var commit = new Commit
            {
                Description = description,
                CreatedOn = DateTime.UtcNow,
                RepositoryId = id,
                CreatorId = creatorId
            };

            this.dbContext.Commits.Add(commit);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<CommitViewModel> GetAllCommits()
        {
            var commits = this.dbContext.Commits
                .Where(c => c.RepositoryId != null && c.CreatorId != null)
                .Select(c => new CommitViewModel
                {
                    Id = c.Id,
                    Name = c.Repository.Name,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn
                })
                .ToList();

            return commits;
        }

        public CommitViewModel GetCommitRepositoryModel(string id)
        {
            var commitRepositoryModel = this.dbContext.Repositories
                .Where(r => r.Id == id)
                .Select(r => new CommitViewModel
                {
                    Name = r.Name,
                    RepositoryId = id
                })
                .FirstOrDefault();

            return commitRepositoryModel;
        }

        public void Delete(string creatorId)
        {
            var commitToDelete = this.dbContext.Commits
                .FirstOrDefault(c => c.CreatorId == creatorId);

            this.dbContext.Commits.Remove(commitToDelete);
            this.dbContext.SaveChanges();
        }
    }
}
