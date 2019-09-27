using Generic_Repository_and_Unit_of_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipezeNyumbaService.Interfaces
{
    public interface IHouseManagement
    {
        List<House> GetAllHouses();
        List<House> GetAllHousesByDistrict(string district);
        List<House> GetAllHousesByDateHouseAvailable(DateTime date);
        List<House> GetAllHousesByDateRange(DateTime startDate, DateTime endDate);
        string UpLoadHouse(House newHouse, HouseContactDetail houseContactDetails);
        string UpDateHouseDetails(int houseID);
        string RemoveHouse(int houseID);

    }
}
