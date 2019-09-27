using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaService.Interfaces.HouseInterfaces.GeneralHouseInterfaces
{
    // Manages the profile of a house
    public interface IHouseProfile
    {
        // Adds new houes in the interface
        bool AddHouse(HouseModel newHouseModel);
        // Remove house in the system based on house Id
        bool RemoveHouse(int id);
        // Updates house profile in the system
        bool UpdateHouseDetails(int id, HouseModel houseDetails);
        // Update house status
        bool UpdateHouseStatus(int houseId, string houseStatus);
    }
}
