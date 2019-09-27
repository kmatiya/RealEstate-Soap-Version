using TipezeNyumbaService.Models.HouseClassesAndMethods;

namespace TipezeNyumbaService.Interfaces.HouseInterfaces.HouseBookingsInterfaces
{
    public interface IHouseBookings
    {
        // Adds a user
        bool BookHouseAppointment(BookHouseModel newBookHouseModel);
    }
}