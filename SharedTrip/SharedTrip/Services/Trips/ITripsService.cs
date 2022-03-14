using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services.Trips
{
    public interface ITripsService
    {
        AllTripsViewModel GetAll();

        void AddTrip(TripInputModel model);

        TripDetailsViewModel GetTrip(string tripId);

        string AddUserToTrip(string tripId, string userId);
    }
}
