namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Issues;
    using System.Collections.Generic;
    using System.Linq;

    public class IssueService : IIssueService
    {
        private readonly ApplicationDbContext dbContext;

        public IssueService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<CarIssueViewModel> GetAllCarIssuesViewModel(string carId)
        {
            var carIssues = this.dbContext.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssueViewModel
                {
                    CarId = c.Id,
                    CarModel = c.Model,
                    Issues = c.Issues.Select(i => new IssueViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsItFixed = i.IsFixed == true ? "Yes" : "Not yet"
                    }).ToList()
                })
                .ToList();

            return carIssues;
        }

        public void AddIssue(string carId, string description)
        {
            var issue = new Issue
            {
                CarId = carId,
                Description = description,
                IsFixed = false
            };

            this.dbContext.Issues.Add(issue);
            this.dbContext.SaveChanges();
        }

        public void FixIssue(string issueId, string carId)
        {
            var newIssue = this.dbContext.Issues
                .Where(i => i.Id == issueId && i.CarId == carId)
                .Select(i => new Issue
                {
                    Id = i.Id,
                    Description = i.Description,
                    IsFixed = true,
                    CarId = i.CarId
                })
                .FirstOrDefault();

            var oldIssue = this.dbContext.Issues
                .FirstOrDefault(i => i.Id == issueId && i.CarId == carId);

            this.dbContext.Remove(oldIssue);
            this.dbContext.Add(newIssue);
            this.dbContext.SaveChanges();
        }

        public void DeleteIssue(string issueId, string carId)
        {
            var issue = this.dbContext.Issues
                .FirstOrDefault(i => i.Id == issueId && i.CarId == carId);

            this.dbContext.Remove(issue);
            this.dbContext.SaveChanges();
        }
    }
}
