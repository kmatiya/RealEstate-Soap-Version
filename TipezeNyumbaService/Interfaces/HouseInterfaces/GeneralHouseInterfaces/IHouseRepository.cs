using System;
using System.Collections.Generic;
using TipezeNyumbaService.Models.HouseClassesAndMethods.GeneralHouses;

namespace TipezeNyumbaService.Interfaces.HouseInterfaces.GeneralHouseInterfaces
{
    public interface IHouseRepository
    {
        // Get All Houses in the system
        List<HouseModel> GetAllHouses();
        // Get houses based on the district house is located
        List<HouseModel> GetAllHousesByDistrict(string district);
        // Get houses based on the date house will be available
        List<HouseModel> GetAllHousesByDateHouseWillBeAvailable(string district,DateTime date, string location = null);
        // Get houses based on the time frame houses will be available
        List<HouseModel> GetAllAvailableHousesByDateRange(string distrcit, DateTime startDate, DateTime endDate, string location = null);
        // Get house by House Id
        HouseModel GetHouseById(int id);
        // Get houses price range 
       List<HouseModel> GetHousesByPriceRange(string district, decimal minimumPrice, decimal maximumPrice, string location = null);
    }
}
