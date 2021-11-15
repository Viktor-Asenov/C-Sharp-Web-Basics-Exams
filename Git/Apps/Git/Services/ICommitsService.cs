using Git.ViewModels.Commits;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string id, string ownerId, string description);

        IEnumerable<CommitViewModel> GetAllCommits();

        CommitViewModel GetCommitRepositoryModel(string id);

        void Delete(string creatorId);
    }
}
