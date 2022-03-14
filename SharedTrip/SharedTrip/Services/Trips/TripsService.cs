using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services.Trips
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext dbContext;

        public TripsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AllTripsViewModel GetAll()
        {
            var allTrips = new AllTripsViewModel
            {
                Trips = this.dbContext.Trips
                    .Select(t => new TripViewModel
                    {
                        Id = t.Id,
                        StartPoint = t.StartPoint,
                        EndPoint = t.EndPoint,
                        DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                        Seats = t.Seats
                    })
                    .ToList()
            };

            return allTrips;
        }

        public void AddTrip(TripInputModel model)
        {
            var dateTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                ImagePath = model.ImagePath,
                DepartureTime = dateTime,
                Seats = model.Seats,
                Description = model.Description
            };

            this.dbContext.Trips.Add(trip);
            this.dbContext.SaveChanges();
        }

        public TripDetailsViewModel GetTrip(string tripId)
        {
            var tripDetails = this.dbContext.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsViewModel
                {
                    Id = t.Id,
                    CarImage = t.ImagePath,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats,
                    Description = t.Description
                })
                .FirstOrDefault();

            return tripDetails;
        }

        public string AddUserToTrip(string tripId, string userId)
        {
            var trip = this.dbContext.Trips
                .FirstOrDefault(t => t.Id == tripId);

            var user = this.dbContext.Users
                .FirstOrDefault(u => u.Id == userId);

            if (this.dbContext.UsersTrips.Any(ut => ut.UserId == user.Id && ut.TripId == trip.Id))
            {
                return null;
            }

            var userTrip = new UserTrip
            {
                UserId = user.Id,
                TripId = trip.Id
            };

            this.dbContext.UsersTrips.Add(userTrip);

            trip.Seats--;

            this.dbContext.SaveChanges();

            return "Success!";
        }
    }
}
