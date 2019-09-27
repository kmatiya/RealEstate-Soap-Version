using System.Collections.Generic;
using TipezeNyumbaService.Models.HouseClassesAndMethods;

namespace TipezeNyumbaService.Interfaces.HouseInterfaces.HouseBookingsInterfaces
{
    public interface IHouseBookingRepository
    {
        // Get booked houses for viewing 
        List<DisplayBookHouseModel> GetBookedHouses();

    }
}