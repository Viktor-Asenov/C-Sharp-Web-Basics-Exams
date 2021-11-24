namespace CarShop.Services
{
    using CarShop.ViewModels.Issues;
    using System.Collections.Generic;

    public interface IIssueService
    {
        IEnumerable<CarIssueViewModel> GetAllCarIssuesViewModel(string carId);

        void AddIssue(string carId, string description);

        void FixIssue(string issueId, string carId);

        void DeleteIssue(string issueId, string carId);
    }
}
